using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class ParryPlayer : MonoBehaviour, IParryable
{
    public InputActionReference ParryAction;

    public Transform swordPivot;

    public float liftAmount = 0.5f;
    public float tiltAngle = 20f;

    public AudioClip HitSound;
    public AudioClip ParrySound;

    private AudioSource audioSource;
    private bool IsParrying = false;
    private bool isAnimating = false;

    private Vector3 normalPosition;
    private Quaternion normalRotation;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        normalPosition = swordPivot.localPosition;
        normalRotation = swordPivot.localRotation;
    }

    private void Update()
    {
        if (ParryAction.action.WasPressedThisFrame() && !isAnimating)
        {
            StartCoroutine(ParryAnimation());
        }
    }

    private IEnumerator ParryAnimation()
    {
        isAnimating = true;
        IsParrying = true;

        // Lift the sword up
        Vector3 raisedPosition = normalPosition + Vector3.up * liftAmount;

        Quaternion raisedRotation = normalRotation * Quaternion.Euler(0, 0, tiltAngle);

        float timer = 0f;

        while (timer < 0.12f)
        {
            timer += Time.deltaTime;

            float progress = timer / 0.12f;

            swordPivot.localPosition = Vector3.Lerp(normalPosition, raisedPosition, progress);
            swordPivot.localRotation = Quaternion.Lerp(normalRotation, raisedRotation, progress);

            yield return null;
        }

        // Hold it
        yield return new WaitForSeconds(0.1f);

        IsParrying = false;

        // Return it
        timer = 0f;

        while (timer < 0.15f)
        {
            timer += Time.deltaTime;

            float progress = timer / 0.15f;

            swordPivot.localPosition = Vector3.Lerp(raisedPosition, normalPosition, progress);
            swordPivot.localRotation = Quaternion.Lerp(raisedRotation, normalRotation, progress);

            yield return null;
        }

        swordPivot.localPosition = normalPosition;
        swordPivot.localRotation = normalRotation;

        isAnimating = false;
    }

    public void ReceiveAttack()
    {
        if (IsParrying)
        {
            audioSource.PlayOneShot(ParrySound);
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else
        {
            audioSource.PlayOneShot(HitSound);
            GetComponent<Renderer>().material.color = Color.red;
        }

        IsParrying = false;
    }
}