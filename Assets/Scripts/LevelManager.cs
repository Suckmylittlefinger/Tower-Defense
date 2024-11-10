using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;

    public int money;

    private void Awake()
    {
        main = this; //Makes sure there is only one instance of LevelManager and makes it more easily accessible in other scripts
    }

    private void Start()
    {
        money = 100; //Starting money
    }

    //Add to total money
    public void AddMoney(int amount)
    {
        money += amount;
    }

    //Spend money
    public bool SpendMoney(int amount) //Bool so we know if purchase was successful
    {
        if (amount <= money)
        {
            // Buy Item
            money -= amount;
            return true;
        }
        else
        {
            Debug.Log("Not enough money");
            return false;
        }

    }
}
