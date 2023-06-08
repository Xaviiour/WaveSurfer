using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyScale : MonoBehaviour
{
    public static float scale = 1;
    public static bool spawnRateIncreased = false;
    float timer = 0;
    float timeForIncrease = 2f;
    float increaseBy = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeForIncrease) 
        {
            scale += increaseBy;
            timer = 0;
        }
    }
}
