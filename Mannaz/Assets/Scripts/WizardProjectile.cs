using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardProjectile : MonoBehaviour
{

    public int shooting = 0;
    public int initialShift = 0;
    public int leftShift = 0;
    public int rightShift = 0;

    void Start()
    {
        
    }

    void Update()
    {

        if (shooting <= 20)
        {
            transform.Translate(0, 0, Time.deltaTime * 10, Space.World);

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

            shooting++;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

    }
}
