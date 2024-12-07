using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int maxHitPoints = 10;
    [SerializeField] private int hitPoints = 10;
    [SerializeField] private int moneyDropped;
    [SerializeField] private bool isTankEnemy;
    [SerializeField] HealthBar healthBar;

    public void TakeDamage(int damage, bool isBigBullet)
    {

        if (isTankEnemy && !isBigBullet)
        {
            //Checks if big bullet for tank enemy and does no damage with normal bullet
            return; 
        }

        hitPoints -= damage;
        healthBar.UpdateHealthBar(hitPoints, maxHitPoints);

        if (hitPoints <= 0)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.AddMoney(moneyDropped); //Adds money on death
            Destroy(gameObject);
        }
    }
}
