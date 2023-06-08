using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CircleShot : MonoBehaviour
{
    [SerializeField] GameObject Obsticle;
    [SerializeField] float offset = 5f;
    public float speed = 5f;
    public float rotationSpeed = 50f;
    bool inactive = true;
    public int memberSize = 5;

    // Circle shot attack
    GameObject[] CircleShotMembers;
    int iter = 0;

    private void Start()
    {
        CircleShotMembers = new GameObject[memberSize];
    }

    // Just runs the shoot code
    void Update()
    {
        Shot();
    }

    // The shoot code
    int ded = 0;
    void Shot()
    {
        if (inactive && iter < CircleShotMembers.Length)
        {
            StartCoroutine(spawnDelay());
            inactive = false;
        }
        // Rotate around
        for (int x = 0; x < iter; x++)
        {
            if (!CircleShotMembers[x].activeInHierarchy) 
            {
                Debug.Log(x);
                ded++;
                continue;
            }

            CircleShotMembers[x].transform.RotateAround(new Vector3(transform.position.x, transform.position.y, CircleShotMembers[x].transform.position.z), new Vector3(0f, 0f, 1f), rotationSpeed * Time.deltaTime);
            CircleShotMembers[x].transform.Translate(Vector3.back * speed * Time.deltaTime);
        }

        // Despawn CircleSHot if unnessessary
        if (ded >= CircleShotMembers.Length) 
        {
            Destroy(gameObject);
        }

        ded = 0;
    }

    // Spawns in the cubes
    IEnumerator spawnDelay()
    {
        // Spawn obsticle
        CircleShotMembers[iter] = SpawnManager.instance.SpawnEnemy();
        CircleShotMembers[iter].transform.position = new Vector3(offset, 0, gameObject.transform.position.z);
        iter++;

        yield return new WaitForSeconds(2);
        inactive = true;
    }

    // Speed Setter
    public void SetSpeed(float projectileSpeed, float projectileRotationSpeed) 
    {
        speed = projectileSpeed;
        rotationSpeed = projectileRotationSpeed;
    }

}
