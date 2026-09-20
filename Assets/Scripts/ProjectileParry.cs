using UnityEngine;

public class ParryProjectile : MonoBehaviour
{
    public float speed = 12f;

    private Rigidbody rb;
    private Transform player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetTarget(Transform target)
    {
        player = target;
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            return;
        }

        Vector3 direction = (player.position - transform.position).normalized;

        rb.MovePosition(
            transform.position + direction * speed * Time.fixedDeltaTime
        );

        RaycastHit hit;

        if (Physics.Raycast(
            transform.position, direction, out hit, 1f))
            {
                IParryable target = hit.collider.GetComponent<IParryable>();

                if (target != null)
                {
                    target.ReceiveAttack();
                    Destroy(gameObject);
                }
            }
        }
}