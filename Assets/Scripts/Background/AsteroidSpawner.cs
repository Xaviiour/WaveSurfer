using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] GameObject asteroid1;
    [SerializeField] GameObject asteroid2;
    [SerializeField] GameObject asteroid3;

    
    [SerializeField] float spawnTime1;
    [SerializeField] float spawnTime2;
    [SerializeField] float spawnTime3;
    // Start is called before the first frame update
    void Start()
    {
        spawnTime1 = 2;
        spawnTime2 = 4;
        spawnTime3 = 6;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime1 -= Time.deltaTime;
        spawnTime2 -= Time.deltaTime;
        spawnTime3 -= Time.deltaTime;

        if (spawnTime1 <= 0)
        {
            var spawnedObject = Instantiate(asteroid1, new Vector3(Random.Range(-30, -80), Random.Range(-20, 30), 100), transform.rotation);
            spawnedObject.transform.parent = gameObject.transform;
            spawnTime1 = 2;
           
        }
        if (spawnTime2 <= 0)
        {
            var spawnedObject = Instantiate(asteroid2, new Vector3(Random.Range(30, 80), Random.Range(-20, 30), 100), transform.rotation);
            spawnedObject.transform.parent = gameObject.transform;
            spawnTime2 = 4;
        }
        if (spawnTime3 <= 0)
        {
            var spawnedObject = Instantiate(asteroid3, new Vector3(Random.Range(30, 80), Random.Range(-20, 30), 100), transform.rotation);
            spawnedObject.transform.parent = gameObject.transform;
            spawnTime3 = 6;
        }
      
    }
}
