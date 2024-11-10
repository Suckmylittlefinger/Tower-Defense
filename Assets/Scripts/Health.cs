using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 10;
    [SerializeField] private int moneyDropped;

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;

        if (hitPoints <= 0)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.AddMoney(moneyDropped); //Adds money on death
            Destroy(gameObject);
        }
    }
}
