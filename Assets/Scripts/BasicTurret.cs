using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.ComponentModel.Design;

public class BasicTurret : MonoBehaviour
{
    //This is a guthub test

    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPreFab;
    [SerializeField] private Transform firePoint;

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 3f;
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] float fireRate = 1f;
    float fireCountdown = 0f;

    private Transform target;

    private void Update()
    {
        //If no target is locked, try to find one
        if (target == null)
        {
            FindTarget();
            return;
        }

        //Rotate the turret towards the locked target
        RotateTowardsTarget();
        
        //If the target moves out of range, reset the target
        if (!CheckTargetIsInRange())
        {
            target = null;
        }

        //if ready to fire will call Shoot method and shoot at enemies 
        if (fireCountdown<= 0f) 
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        //slowly decreases the fireCountdown each frame to get ready to fire again
        fireCountdown -= Time.deltaTime;
        
    }

    //Finds the closest target within the turret's targeting range
    private void FindTarget()
    {
        //Casts a 2D circle to detect all enemies in range based on the enemyMask
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2) transform.position, 0f, enemyMask);

         //If any enemies were detected, set the first one as the target
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    //Checks if the current target is still within the targeting range
    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    //Rotates the turret towards the target
    private void RotateTowardsTarget()
    {
        //Calculate the angle between the turret and the target in degrees
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        //Create a rotation towards the calculated angle
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        //Rotate the turret over time at the specified rotation speed
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    //Visualize the targeting range when the turret is selected in the editor
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.green;
        //Draw a wireframe disc around the turret to represent its range
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
    //shoots bullets at the targetted enemy 
    private void Shoot()
    {// to get reference to bullet object and access script
        GameObject bulletGO = (GameObject)Instantiate(bulletPreFab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target); //sends target the turret is aiming at to the bullet script 
        }
    }
}
