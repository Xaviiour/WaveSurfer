using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    [SerializeField]
    private ObjectPoolManager bulletPool,
        enemyPool,
        powerupPool;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public Rigidbody SpawnPlayerBullet(Vector3 spawnPos)
    {
        GameObject bullet = bulletPool.GetObjectFromPool();
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bullet.transform.position = new Vector3(spawnPos.x, spawnPos.y, spawnPos.z + 1);

        return bulletRb;
    }

    public void DespawnBullet(GameObject bullet)
    {
        bulletPool.ReturnObjectToPool(bullet);
        bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public GameObject SpawnEnemy()
    {
        return enemyPool.GetObjectFromPool();
    }

    public void DespawnEnemy(GameObject enemy)
    {
        enemy.GetComponent<Rigidbody>().velocity = Vector3.zero;
        enemyPool.ReturnObjectToPool(enemy);
    }

    public void ClearAllEnemies()
    {
        foreach (Enemy enemy in FindObjectsOfType<Enemy>())
        {
            if (enemy.gameObject.activeInHierarchy)
            {
                enemy.GetComponent<Rigidbody>().velocity = Vector3.zero;
                DespawnEnemy(enemy.gameObject);
                //SpawnPowerup(enemy.transform.position);
            }
                
        }
    }

    public void SpawnPowerup(Vector3 spawnPos)
    {
        if (Random.Range(0, 5) == 0)    // 20% chance of a powerup spawning 
        {
            GameObject powerup = powerupPool.GetObjectFromPool();
            powerup.transform.position = spawnPos;
        }
        
    }

    public void DespawnPowerup(GameObject powerup)
    {
        powerupPool.ReturnObjectToPool(powerup);
    }
}
