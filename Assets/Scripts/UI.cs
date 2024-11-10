using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI moneyUI;

    private void Update()
    {
        moneyUI.text = LevelManager.main.money.ToString();
    }
}
