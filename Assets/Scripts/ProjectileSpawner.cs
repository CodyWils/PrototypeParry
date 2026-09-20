using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject projectile;
    public Transform player;

    public float spawnTime = 1.5f;
    public float spawnDistance = 10f;

    private float timer;

    private void Start()
    {
        timer = -2f;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnTime)
        {
            SpawnProjectile();
            timer = 0;
        }
    }

    private void SpawnProjectile()
    {
        Vector3 direction = (player.position - transform.position).normalized;

        Vector3 position = player.position - direction * spawnDistance;

        GameObject newProjectile =
            Instantiate(projectile, position, Quaternion.identity);

        newProjectile.GetComponent<ParryProjectile>().SetTarget(player);
    }
}