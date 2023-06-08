using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public float gameRunTime;
    [SerializeField] SpawnManager spawnManager;
    [SerializeField] GameObject boss;
    [SerializeField] float bossSpawnTimer = 120;
    // Update is called once per frame

    public enum State { LEVEL, BOSS, GAMEOVER, WIN }
    public State currentState;
    bool enemiesClear;

    private void Awake()
    {
        currentState = State.LEVEL; //When level starts, the level is in its base phase
    }
    void Update()
    {
        switch(currentState)
        {
            case State.LEVEL:
                boss.SetActive(false);
                gameRunTime += Time.deltaTime;

                if (!AudioManager.instance.sounds[0].source.isPlaying)
                    AudioManager.instance.PlayClip("Level");
                if (gameRunTime >= bossSpawnTimer) //When 2 minutes pass, the level changes into its boss phase
                {
                    currentState = State.BOSS;
                }
                break;

            case State.BOSS:
                if(currentState == State.BOSS)
                {
                    boss.SetActive(true);

                    if (!enemiesClear)
                    {
                        spawnManager.ClearAllEnemies();
                        enemiesClear = true;
                    }

                    AudioManager.instance.FadeOutAudio("Level", 1f);
                    AudioManager.instance.PlayClip("Boss");
                }
                break;

            case State.GAMEOVER:
                SceneManager.LoadScene("Game Over");
                break;

            case State.WIN:
                SceneManager.LoadScene("Win Screen");
                break;
        }
       
    }
}
