using System.Collections;
using UnityEngine;

public class Wizard : MonoBehaviour
{

    public int health = 30;
    public float speed = 4f;
    public int damage = 10;
    public float attackRate = 0.8f;
    public float attackRange = 2f;
    public float spawnRate = 6f;
    public int attackDelay = 0;
    public int shooting = 0;
    public bool attacking = true;

    public WizardProjectile fireball;

    void Start()
    {
        StartCoroutine(CoUpdate());
    }

    IEnumerator CoUpdate()
    {
        while (true)
        {

            if ( health <= 0 )
            {
                Destroy(gameObject);
                yield return null;
            }

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

                // spawn new ball
                WizardProjectile newArrow = Instantiate(fireball, position, rotation) as WizardProjectile;
                yield return new WaitForSeconds(3f);
                attacking = false;

            }

            yield return null;

        }

    }

}
