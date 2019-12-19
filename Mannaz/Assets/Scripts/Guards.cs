using System.Collections;
using UnityEngine;

public class Guards : MonoBehaviour
{

    public int health = 100;
    public float speed = 4f;
    public int damage = 15;
    public float attackRate = 1f;
    public float attackRange = 1f;
    public float spawnRate = 4f;
    public int attackDelay = 0;
    public int pullback = 0;
    public int forward = 0;
    public int backward = 0;
    public bool attacking = true;

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

                if (attackDelay >= 30)
                {
                    attacking = true;
                    attackDelay = 0;
                    yield return new WaitForSeconds(0.5f);
                }

            }

            // initiate attack
            else
            {
                // attacking "animation"
                if (pullback < 10)
                {
                    transform.position -= transform.forward * Time.deltaTime;
                    pullback++;
                }
                else if (forward < 3)
                {
                    transform.position += transform.forward * Time.deltaTime * 30;
                    forward++;
                }
                else if (backward < 6)
                {
                    transform.position -= transform.forward * Time.deltaTime * 13;
                    backward++;
                }
                else
                {
                    yield return new WaitForSeconds(1f);
                    attacking = false;
                    pullback = 0;
                    forward = 0;
                    backward = 0;
                }
            }

            yield return null;

        }

    }

}
