using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower : MonoBehaviour
{
    private Transform target;
    [Header("Attributes")]
    public float range = 10f;
    public float fireRate = 1f;
    private float fireCoolDown = 0f;
    [Header("Setup")]
    public string enemyTag = "Enemy";

    public GameObject bulletPrefab;
    public Transform firePoint;
    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject nearestEnemy = null;
        float distance = Mathf.Infinity;

        foreach(GameObject enemy in enemies)
        {
            float d = Vector3.Distance(transform.position, enemy.transform.position);
            if (nearestEnemy == null || d < distance)
            {
                nearestEnemy = enemy;
                distance = d;
            }
        }
        if (nearestEnemy != null && distance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }

        Vector3 dir = target.position - transform.position;
        
        if (fireCoolDown <= 0f)
        {
            Shoot();
            fireCoolDown = 1f / fireRate;
        }

        fireCoolDown -= Time.deltaTime;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void Shoot()
    {
        GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGo.GetComponent<Bullet>();
        if(bullet!= null)
        {
            bullet.Chase(target);
        }
    }
}
