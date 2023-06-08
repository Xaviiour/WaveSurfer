using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopMusic : MonoBehaviour
{
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(StopTitleMusic);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StopTitleMusic()
    {
        AudioManager audioManager = GameObject.FindObjectOfType<AudioManager>();
        audioManager.StopClip("Title");
    }
}
