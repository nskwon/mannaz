using System.Collections;
using UnityEngine;

public class Bowman : MonoBehaviour
{

    public int health = 50;
    public float speed = 4f;
    public int damage = 10;
    public float attackRate = 1.5f;
    public float attackRange = 1f;
    public float spawnRate = 6f;
    public int attackDelay = 0;
    public int shooting = 0;
    public bool attacking = true;

    public BowmanProjectile arrow;

    void Start()
    {
        StartCoroutine(CoUpdate());
    }

    IEnumerator CoUpdate()
    {
        while (true)
        {
            // check to move forward
            if (!attacking)
            {
                transform.position += transform.forward * Time.deltaTime * speed;
                attackDelay++;

                if (attackDelay >= 40)
                {
                    
                    attacking = true;
                    attackDelay = 0;

                }

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
                yield return new WaitForSeconds(3f);
                attacking = false;
                
            }

            yield return null;

        }

    }

}
