using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health, numOfHearts;

    [SerializeField] private Image[] hearts;
    [SerializeField] heartShow[] heartSprites;
    [SerializeField] private Sprite fullHeart, emptyHeart;
    [SerializeField] LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        heartSprites = new heartShow[hearts.Length];
        for (int x = 0; x < heartSprites.Length; x++) 
        {
            heartSprites[x] = hearts[x].gameObject.GetComponent<heartShow>();
        }
    }

    private void OnEnable()
    {
        EnemyCollision.PlayerEnemyCollision += LoseHearts;
    }

    private void OnDisable()
    {
        EnemyCollision.PlayerEnemyCollision -= LoseHearts;
    }

    // Update is called once per frame
    void Update()
    {

        if (health > numOfHearts)
            health = numOfHearts;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
                hearts[i].enabled = true;
                //heartSprites[i].SetHeartVisibility(true);

            }
            else
            {
                hearts[i].enabled = true;
                //heartSprites[i].SetHeartVisibility(false);
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                //heartSprites[i].SetHeartVisibility(true);
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
                //heartSprites[i].SetHeartVisibility(false);
            }

            hearts[i].enabled = !heartSprites[i].SetHeartVisibility(hearts[i].sprite != emptyHeart);
        }

        if (health <= 0)
        {
            levelManager.currentState = LevelManager.State.GAMEOVER;
        }

        if (health == 1)
        {
            if (!UgVisuals.switchedMainAndLowH)
            {
                UgVisuals.SwitchMainAndLowH();
            }
        }
        else
        {
            if (UgVisuals.switchedMainAndLowH) 
            {
                UgVisuals.SwitchMainAndLowH();
            }
        }
    }

    private void LoseHearts()
    {
        health--;
    }

    private void GainHearts()
    {
        health++;
    }
}
