using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject nextLevelScreen;

    public Transform startPoint;
    public Transform[] path;

    public int money;
    public int totalLives;

    private void Awake()
    {
        main = this; //Makes sure there is only one instance of LevelManager and makes it more easily accessible in other scripts
    }

    private void Start()
    {
        money = 150; //Starting money
        totalLives = 3; //Starting lives
    }

    private void Update()
    {
        if (totalLives == 0)
        {
            GameOver();
        }

        //Extra money Cheat Code
        if (Input.GetKeyDown(KeyCode.M))
        {
            money += 250;
        }

        //Extra Lives Cheat Code
        if (Input.GetKeyDown(KeyCode.L))
        {
            totalLives += 1;
        }
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
            return false;
        }
    }

    public void LoseLives(int lifeLost)
    {
        totalLives -= lifeLost;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }

    public void LevelComplete()
    {
        Time.timeScale = 0;
        nextLevelScreen.SetActive(true);
    }
}
