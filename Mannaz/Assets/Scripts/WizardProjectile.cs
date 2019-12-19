using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardProjectile : MonoBehaviour
{

    public int shooting = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (shooting <= 50)
        {
            //transform.position += transform.forward * Time.deltaTime * 10;
            //var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //transform.position += move * 10 * Time.deltaTime;
            transform.Translate(0, 0, Time.deltaTime * 10, Space.World);
            transform.Rotate(0, 10, 0);
            shooting++;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

    }
}
