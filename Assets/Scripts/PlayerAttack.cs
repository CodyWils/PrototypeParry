using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public Transform swordPivot;

    public float swingDuration = 0.25f;
    public float startAngle = 45f;
    public float endAngle = -135f;

    public float attackDistance = 1f;

    private bool isAttacking = false;
    private Quaternion normalRotation;

    private void Start()
    {
        normalRotation = swordPivot.localRotation;
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.mKey.wasPressedThisFrame && !isAttacking)
        {
            StartCoroutine(SwingSword());
        }
    }

    private IEnumerator SwingSword()
    {
        isAttacking = true;

        // Damage enemies within range
        Collider[] enemies = Physics.OverlapSphere(transform.position, attackDistance);

        foreach (Collider enemy in enemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                SpawnerHealth health = enemy.GetComponentInParent<SpawnerHealth>();

                if (health != null)
                {
                    health.TakeHit();
                    break;
                }
            }
        }

        // Instantly move sword
        swordPivot.localRotation = normalRotation * Quaternion.Euler(0, startAngle, 0);

        float timer = 0f;

        // Swing through wide arc
        while (timer < swingDuration)
        {
            timer += Time.deltaTime;

            float progress = timer / swingDuration;

            float angle = Mathf.Lerp(startAngle, endAngle, progress);

            swordPivot.localRotation = normalRotation * Quaternion.Euler(0, angle, 0);

            yield return null;
        }

        // return to normal
        swordPivot.localRotation = normalRotation;

        isAttacking = false;
    }
}