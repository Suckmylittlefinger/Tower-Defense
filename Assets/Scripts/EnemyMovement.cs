using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform target; //Current target point in path
    private int pathIndex = 0; //Index to track the current position in path

    private void Start()
    {
        target = LevelManager.main.path[pathIndex]; //Set the first target point from the path defined in LevelManager
    }

    private void Update()
    {
        //Check if the enemy is close to the current target point
        if (Vector2.Distance(target.position, transform.position) <= 0.1)
        {
            pathIndex++; //Move to the next point in the path

            if (pathIndex == LevelManager.main.path.Length) //Check if the enemy has reached the end of the path
            {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject); //Destroy enemy when it reaches end of path
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex]; //Set the next target point in the path
            }
        }
    }

    private void FixedUpdate()
    {
        //Calculates the direction to the next target point
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * moveSpeed; //Moves the enemy
    }
}
