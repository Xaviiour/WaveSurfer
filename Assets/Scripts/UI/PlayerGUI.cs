using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerGUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bombsAmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        BombPowerup.BombCollected += IncrementBombsText;
        Shooting.BombUsed += DecrementBombsText;
    }

    private void OnDisable()
    {
        BombPowerup.BombCollected -= IncrementBombsText;
        Shooting.BombUsed -= DecrementBombsText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void IncrementBombsText()
    {
        bombsAmount.text = $"{System.Int32.Parse(bombsAmount.text) + 1}";
    }

    private void DecrementBombsText()
    {
        bombsAmount.text = $"{System.Int32.Parse(bombsAmount.text) - 1}";
    }
}
