using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject projectile;
    public Transform player;

        // Updates here
    public float minSpawnTime = 0.3f;
    public float maxSpawnTime = 2.4f;
    public float spawnDistance = 20f;

        // Updates here
    private float spawnTime;
    private float timer;

    private void Start()
    {
        // Updates here
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        timer = -2f;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnTime)
        {
            SpawnProjectile();
            timer = 0;

            // Updates here
            spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
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