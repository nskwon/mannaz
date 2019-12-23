using System.Collections;
using UnityEngine;

public class Guards : MonoBehaviour
{

    public int health = 100;
    public float speed = 4f;
    public int damage = 15;
    public float attackRate = 1f;
    public float attackRange = 5.5f;
    public float spawnRate = 4f;
    public int pullback = 0;
    public int forward = 0;
    public int backward = 1;
    public bool attacking = false;

    private Transform target;
    public string enemyTag = "Enemy";
    Quaternion attackRot;
    Quaternion initialRot;
    Vector3 initialPos;
    public GameObject GuardImpactEffect;
    public GameObject deathEffect;

    void Start()
    {
        initialRot = transform.rotation;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        StartCoroutine(CoUpdate());
    }

    void UpdateTarget()
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (!attacking)
        {
            if (nearestEnemy != null && shortestDistance <= attackRange)
            {
                target = nearestEnemy.transform;
            }
            else
            {
                target = null;
            }
        } else
        {
            if (nearestEnemy != null && shortestDistance <= attackRange+0.75f)
            {
                target = nearestEnemy.transform;
            }
            else
            {
                target = null;
            }
        }

    }

    void Update()
    {

        if (health <= 0)
        {
            GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 3.5f);
            Destroy(gameObject);
            return;
        }

        if ( target != null )
        {
            if (transform.position != target.position)
            {
                Vector3 dir = target.position - transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(dir);
                Vector3 rotation = lookRotation.eulerAngles;
                transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            }
        }
    }

    IEnumerator CoUpdate()
    {
        while (true)
        {

            if (target == null)
            {

                if (pullback >= 16 && forward > 10 && (pullback != 0 && forward != 0 && backward != 0))
                {
                    if (backward < 35)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, initialPos, Time.deltaTime * 11);
                        backward++;
                        transform.rotation = attackRot;
                    }
                    else
                    {
                        pullback = 0;
                        forward = 0;
                        backward = 0;
                    }
                }
                else
                {
                    attacking = false;
                    yield return null;
                }
            }
            else
            {
                attacking = true;

                if (pullback == 0 || !attacking)
                {
                    initialPos = transform.position;
                }

                // attacking "animation"
                if (pullback < 16)
                {
                    transform.position -= transform.forward * Time.deltaTime;
                    pullback++;
                }
                else if (forward < 10)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * 28);
                    forward++;
                    if (transform.position == target.position)
                    {
                        forward = 10;
                    }
                    attackRot = transform.rotation;
                }
                else if (forward == 10)
                {
                    HitTarget();
                    Damage(target);
                    forward++;
                    backward = 1;
                }
                else if (backward < 35)
                {
                    transform.position = Vector3.MoveTowards(transform.position, initialPos, Time.deltaTime * 11);
                    backward++;
                }
                else
                {
                    pullback = 0;
                    forward = 0;
                    backward = 0;
                    yield return new WaitForSeconds(1.0f);
                }
            }

            // check to move forward
            if (!attacking)
            {
                if (backward == 0)
                {
                    transform.rotation = initialRot;
                }
                transform.position += transform.forward * Time.deltaTime * speed;
            }

            yield return null;

        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void Damage(Transform enemy)
    {
        DummyEnemy e = enemy.GetComponent<DummyEnemy>();

        if (e != null)
        {
            e.TakeDamage(damage);
        }

    }

    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(GuardImpactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 0.25f);
    }

}
