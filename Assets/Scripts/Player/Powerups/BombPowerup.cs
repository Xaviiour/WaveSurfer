using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BombPowerup : Powerup
{
    public static event Action BombCollected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)    // if player collision
        {
            BombCollected?.Invoke();
            SpawnManager.instance.DespawnPowerup(gameObject);
        }
    }
}
