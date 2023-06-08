using UnityEngine;

public class Bullets : MonoBehaviour
{
    private void Update()
    {
        DestroyLongRange();
    }

    private void DestroyLongRange()
    {
        if(transform.position.z > 50f)
        {
            SpawnManager.instance.DespawnBullet(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        SpawnManager.instance.DespawnBullet(gameObject);
    }
}
