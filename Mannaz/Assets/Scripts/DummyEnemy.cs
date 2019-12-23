using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyEnemy : MonoBehaviour
{

    public int health = 50;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if ( health <= 0 )
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            return;
        }
        transform.position -= transform.forward * Time.deltaTime;
    }
}
