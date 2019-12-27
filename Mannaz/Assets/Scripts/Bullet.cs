using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    private Transform target;
    public GameObject Effect;

    public void Chase(Transform _target)
    {
        target = _target;
    }
    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            ShootTarget();
            return;
        }
        else
        {
            transform.Translate(dir.normalized * distanceThisFrame, Space.World);   
        }
    }

    void ShootTarget()
    {
        GameObject holder = (GameObject)Instantiate(Effect, transform.position, transform.rotation);
        Destroy(holder);
        Destroy(gameObject);
    }
}
