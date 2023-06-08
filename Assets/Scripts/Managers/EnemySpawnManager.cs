using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

/*
Alex Gulewich
Feb, 24, 2023
Enemy Spawner
Spawns in enemeies
 */

public class EnemySpawnManager : MonoBehaviour
{
    // Gameobjects
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject ship;
    GameObject enemyContainer;
    [SerializeField] int maxShips = 5;
    int spawnedSHips = 0;
    GameObject[] ships;

    // Spawn atributes
    [SerializeField] float spawnRateTMP;
    WaitForSeconds spawnRate;

    float lowerBound, upperBound;
    // Start is called before the first frame update
    void Start()
    {
        // Set variables
        spawnRate = new WaitForSeconds(spawnRateTMP);
        lowerBound = transform.GetChild(0).transform.position.x;
        upperBound = transform.GetChild(1).transform.position.x;

        // Setup the enemy storeage transform
        enemyContainer = new GameObject("Enemy Container");
        enemyContainer.transform.SetParent(gameObject.transform);

        ships = new GameObject[maxShips];

        for (int x = 0; x < ships.Length; x++)
        {
            ships[x] = Instantiate(ship);
        }

        // Start spawning enemeis
        StartCoroutine(SpawnEnemy());
    }

    GameObject enTMP; // Enemy tmp storage
    float dt = 0;
    IEnumerator SpawnEnemy()
    {
        while (true)
        {

            //enTMP = Instantiate(enemyPrefab, enemyContainer.transform);
            //enTMP.transform.position = new Vector3(Random.Range(lowerBound, upperBound), Random.Range(-5, 5), Random.Range(20, 120));
            //enTMP = Instantiate(enemyPrefab);
            //enTMP.transform.position = new Vector3(Random.Range(lowerBound, upperBound), Random.Range(lowerBound * 0.75f, upperBound * 0.75f), Random.Range(20, 120));

            if (DifficultyScale.spawnRateIncreased) 
            {
                DifficultyScale.spawnRateIncreased = false;
                spawnRate = new WaitForSeconds(spawnRateTMP - (1 - DifficultyScale.scale));
            }

            int toSpawn = 0;
            float scale;
            if (DifficultyScale.scale < 24f)
            {
                scale = DifficultyScale.scale;
            }
            else 
            {
                scale = 24;
            }

            if ( dt > 3f - scale) 
            {
                dt = 0;
                toSpawn++;
            }
            for (int x = 0; x < ships.Length && toSpawn > 0; x++) 
            {
                if (ships[x].activeInHierarchy)
                {
                    continue;
                }

                ships[x].SetActive(true);
                ships[x].transform.position = new Vector3(Random.Range(lowerBound, upperBound), Random.Range(lowerBound * 0.5f, upperBound * 0.5f), Random.Range(40, 170));
                ships[x].GetComponent<EnemyShip>().Innit();
                toSpawn--;
                
               
            }

            dt += 1;

            enTMP = SpawnManager.instance.SpawnEnemy();
            enTMP.transform.position = new Vector3(Random.Range(lowerBound, upperBound), Random.Range(lowerBound * 0.5f, upperBound * 0.5f), Random.Range(40, 170));
            enTMP.transform.localScale = new Vector3(2f, 2f, 2f);
            enTMP.GetComponent<Enemy>().selfMoving = true;
            yield return spawnRate;
        }
    }
}
