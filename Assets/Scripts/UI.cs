using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI moneyUI;
    [SerializeField] TextMeshProUGUI livesUI;

    private void Update()
    {
        moneyUI.text = "Money: " + LevelManager.main.money.ToString();
        livesUI.text = "Lives: " + LevelManager.main.totalLives.ToString();
    }
}
