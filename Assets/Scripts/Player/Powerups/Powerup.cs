using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private float despawnRange = -6.0f;
    private Rigidbody powerupRb;

    private void Awake()
    {
        powerupRb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DespawnOutOfBounds();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        powerupRb.MovePosition(transform.position + Vector3.back * moveSpeed * Time.deltaTime);
    }

    private void DespawnOutOfBounds()
    {
        // Destroy self upon going beyond player's view
        if (transform.position.z < despawnRange)
        {
            SpawnManager.instance.DespawnEnemy(gameObject);
        }
    }
}
