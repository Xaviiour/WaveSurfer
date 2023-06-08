using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    public float speed = 2f;
    float rotZ;

    void Start()
    {
        rotZ = Random.Range(-15f, 45f);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position -= transform.forward * speed * Time.deltaTime;
        transform.Rotate(transform.rotation.x, transform.rotation.y, rotZ * Time.deltaTime);
    }
}
