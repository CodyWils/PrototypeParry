using UnityEngine;

public class SpawnerHealth : MonoBehaviour
{
    public int hitsRequired = 3;

    private int hitsTaken = 0;

    public void TakeHit()
    {
        hitsTaken++;

        if (hitsTaken >= hitsRequired)
        {
            Destroy(gameObject);
        }
    }
}