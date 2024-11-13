using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    [SerializeField] float speed = 10f;
    [SerializeField] int bulletDamage = 1;
    [SerializeField] bool isBigBullet;
    [SerializeField] GameObject impactEffect;

    public void Seek (Transform _target)
    {
        target = _target;
    }

     public bool IsBigBullet()
    {
        return isBigBullet;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    private void HitTarget()
    {
        GameObject effectInstance = (GameObject) Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 2f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
{
    var health = other.gameObject.GetComponent<Health>();
    if (health != null)
    {
        health.TakeDamage(bulletDamage, isBigBullet);
    }
}
}
