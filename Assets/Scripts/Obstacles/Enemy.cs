using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Alex Gulewich
Feb, 24, 2023
Enemy
Simple enemy that moves towards player
 */

public class Enemy : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed = 5f;
    private float despawnRange = -6.0f;
    public bool selfMoving = true;
    public float maxSpeed = 30f;
    bool dead = false;

    // Components
    ParticleSystem death;
    SpriteRenderer sr;
    BoxCollider bc;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        death = transform.GetChild(0).GetComponent<ParticleSystem>();
        bc = GetComponent<BoxCollider>();
        sr = GetComponent<SpriteRenderer>();

        death.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (selfMoving && !dead)
        {
            float s = DifficultyScale.scale;

            if (speed * DifficultyScale.scale > maxSpeed) 
            {
                s = maxSpeed / speed;
            }

            rb.MovePosition(transform.position + Vector3.back * (speed * s) * Time.deltaTime);
        }
        //rb.AddForce(Vector3.back * speed * Time.deltaTime);

        // Destroy self upon going beyond player's view
        if (transform.position.z < despawnRange)
        {
            SpawnManager.instance.DespawnEnemy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 6)
        {
            
            SpawnManager.instance.DespawnBullet(collision.gameObject);
            StartCoroutine(playDeathAnimation());
        }
    }

    IEnumerator playDeathAnimation() 
    {
        sr.enabled = false;
        bc.enabled = false;
        dead = true;

        AudioManager.instance.PlayClip("EnemyDead");
        SpawnManager.instance.SpawnPowerup(gameObject.transform.position);

        death.gameObject.SetActive(true);
        death.Play();

        yield return new WaitForSeconds(6);

        sr.enabled = true;
        bc.enabled = true;
        dead = false;
        death.Stop();
        SpawnManager.instance.DespawnEnemy(gameObject);
        
    }
}
