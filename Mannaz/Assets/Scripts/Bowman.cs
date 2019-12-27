using System.Collections;
using UnityEngine;

public class Bowman : MonoBehaviour
{

    public int health = 50;
    public float speed = 4f;
    public float attackRate = 1.5f;
    public float attackRange = 16.0f;
    public float spawnRate = 6f;
    public int shooting = 0;
    public bool attacking = false;

    public Transform target;
    public string myTag = "MyTroop";
    public string mirrorTag = "MirrorTroop";
    Quaternion initialRot;

    public BowmanProjectile arrow;
    public GameObject deathEffect;

    void Start()
    {
        if ( gameObject.tag == "MirrorTroop" )
        {
            transform.Rotate(180.0f, 0f, 0f, Space.World);
        }

        initialRot = transform.rotation;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        StartCoroutine(CoUpdate());
    }

    void UpdateTarget()
    {

        GameObject[] enemies;

        if (gameObject.tag == "MyTroop")
        {
            enemies = GameObject.FindGameObjectsWithTag(mirrorTag);
        }
        else if (gameObject.tag == "MirrorTroop")
        {
            enemies = GameObject.FindGameObjectsWithTag(myTag);
        } else
        {
            enemies = null;
        }        
        
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        if (enemies != null)
        {
            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }

            if ( target != null && Vector3.Distance(transform.position, target.position) > attackRange )
            {
                target = null;
            }

            if (nearestEnemy != null && shortestDistance <= attackRange )
            {
                if (target == null)
                {
                    target = nearestEnemy.transform;
                } 
            }
            else
            {
                target = null;
            }
        }

    }

    void Update()
    {

        if ( health <= 0 )
        {
            GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 3.5f);
            Destroy(gameObject);
            return;
        }

        if ( target == null )
        {
            transform.rotation = initialRot;
            attacking = false;
            return;
        }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

    }

    IEnumerator CoUpdate()
    {
        while (true)
        {

            if (target == null)
            {
                yield return null;
            }
            else
            {
                attacking = true;
            }

            if (health <= 0)
            {
                Destroy(gameObject);
                yield return null;
            }

            // check to move forward
            if (!attacking)
            {
                transform.position += transform.forward * Time.deltaTime * speed;
            }

            // initiate attack
            else
            {

                yield return new WaitForSeconds(0.5f);

                // attacking "animation"
                Vector3 position = transform.position;
                Quaternion rotation = transform.rotation;

                // spawn new arrow
                BowmanProjectile newArrow = Instantiate(arrow, position, rotation) as BowmanProjectile;
                if ( newArrow != null )
                {
                    newArrow.Seek(target);
                }
                yield return new WaitForSeconds(3f);
                attacking = false;
                
            }

            yield return null;

        }

    }

    public void TakeDamage(int amount)
    {
        health -= amount;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
