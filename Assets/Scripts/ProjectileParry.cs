using UnityEngine;

public class ParryProjectile : MonoBehaviour
{
        // Updates here
    public float minSpeed = 12f;
    public float maxSpeed = 32f;

    private float speed;

    private Rigidbody rb;
    private Transform player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Updates here
        speed = Random.Range(minSpeed, maxSpeed);
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

        float distance = speed * Time.fixedDeltaTime;

        // Update Here
        RaycastHit hit;

        if (Physics.SphereCast(
            transform.position,
            0.3f,
            direction,
            out hit,
            distance + 0.3f))
        {
            IParryable target = hit.collider.GetComponentInParent<IParryable>();

            if (target != null)
            {
                target.ReceiveAttack();
                Destroy(gameObject);
                return;
            }
        }

        // Move the projectile only if it has not hit the player.
        rb.MovePosition(transform.position + direction * distance);
    }

        // Update here
    private void OnCollisionEnter(Collision collision)
    {
        IParryable target = collision.gameObject.GetComponent<IParryable>();

        if (target != null)
        {
            target.ReceiveAttack();
            Destroy(gameObject);
        }
    }
}