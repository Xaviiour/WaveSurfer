using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false;
    public bool IsPaused { get { return isPaused; } } 

    [SerializeField] private GameObject pauseMenuUI;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }

        if (!pauseMenuUI.activeInHierarchy)
        {
            Time.timeScale = 1f;

            //AudioManager.instance.SetVolume("Level", 1f);
            //AudioManager.instance.SetVolume("Boss", 1f);
        }
            
    }

    public void Resume()
    {
        // set pause panel menu to inactive
        pauseMenuUI.SetActive(false);

        // resume in-game time
        Time.timeScale = 1f;

        // set bool "isPaused" to false
        isPaused = false;
    }

    private void Pause()
    {
        // set pause panel menu to active
        pauseMenuUI.SetActive(true);

        // freeze in-game time
        Time.timeScale = 0f;

        // lower volume a bit
        //AudioManager.instance.SetVolume("Level", 0.5f);
       // AudioManager.instance.SetVolume("Boss", 0.5f);

        // set bool "isPaused" to true
        isPaused = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);  // go back to title screen
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game.");
        Application.Quit();
    }
}
