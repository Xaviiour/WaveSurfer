using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField] private float invulnerabilityFrames = 0.5f;
    private SpriteRenderer playerSr;

    private bool isInvulnerable = false;
    private float blinkInterval = 0.5f;
    private WaitForSeconds blinkDuration;

    public static event Action PlayerEnemyCollision;

    private void Awake()
    {
        playerSr = GetComponent<SpriteRenderer>();
        blinkDuration = new WaitForSeconds(blinkInterval);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 9 && !isInvulnerable)
        {
            // fire event that tells player health to reduce health
            PlayerEnemyCollision?.Invoke();

            AudioManager.instance.PlayClip("PlayerHurt");
            StartCoroutine(StartInvulnerability());
        }
    }

    private IEnumerator StartInvulnerability()
    {
        isInvulnerable = true;

        for (float i = 0; i < invulnerabilityFrames; i += blinkInterval)
        {
            playerSr.enabled = !playerSr.enabled;
            yield return blinkDuration;
        }

        playerSr.enabled = true;
        isInvulnerable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInvulnerable)
            playerSr.enabled = !playerSr.enabled;
    }
}
