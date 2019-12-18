using System.Collections;
using UnityEngine;

public class Guards : MonoBehaviour
{

    public int health = 100;
    public float speed = 4f;
    public int damage = 15;
    public float rate = 1f;
    public float range = 1f;
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

                if (attackDelay >= 50)
                {
                    attacking = true;
                    attackDelay = 0;
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
                else if (forward < 2)
                {
                    transform.position += transform.forward * Time.deltaTime * 30;
                    forward++;
                }
                else if (backward < 5)
                {
                    transform.position -= transform.forward * Time.deltaTime * 10;
                    backward++;
                }
                else
                {
                    yield return new WaitForSeconds(0.5f);
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
