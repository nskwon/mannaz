using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardProjectile : MonoBehaviour
{

    public int shooting = 0;
    public int initialShift = 0;
    public int leftShift = 0;
    public int rightShift = 0;
    private Transform target;
    public GameObject WizardImpactEffect;
    public int damage = 5;


    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Start()
    {
        
    }

    void Update()
    {

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = Time.deltaTime * 10;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            Damage(target);
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        if ( initialShift <= 2)
        {
            transform.Translate(Time.deltaTime * 6, 0, 0, Space.World);
            initialShift++;
        } else if ( leftShift <= 4 )
        {
            transform.Translate(-Time.deltaTime * 6, 0, 0, Space.World);
            leftShift++;
        } else if ( rightShift <= 4 )
        {
            transform.Translate(Time.deltaTime * 6, 0, 0, Space.World);
            rightShift++;
        } else
        {
            leftShift = 0;
            rightShift = 0;
        }

    }

    void Damage(Transform enemy)
    {
        enemy.SendMessage("TakeDamage", damage);
     }

    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(WizardImpactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);
        Destroy(gameObject);
        return;
    }

}
