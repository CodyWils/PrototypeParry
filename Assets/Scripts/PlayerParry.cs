using UnityEngine;
using UnityEngine.InputSystem;

public class ParryPlayer : MonoBehaviour, IParryable
{
    public InputActionReference ParryAction;

    public AudioClip HitSound;
    public AudioClip ParrySound;

    private AudioSource audioSource;
    private bool IsParrying = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (ParryAction.action.WasPressedThisFrame())
        {
            IsParrying = true;
            StartCoroutine(ParryTimer());
        }
    }

    private System.Collections.IEnumerator ParryTimer()
    {

        yield return new WaitForSeconds(0.2f);

        IsParrying = false;
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