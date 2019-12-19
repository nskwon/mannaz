using System.Collections;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    //private GameObject building;
    private Renderer rend;
    private Color startColor;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    void OnMouseDown()
    {
        GameObject turretToBuild = BuildManager.instance.GetBuildingToBuild();
        GameObject turret = (GameObject)Instantiate(turretToBuild, transform.position, transform.rotation);
    }
    // Update is called once per frame
    void OnMouseEnter()
    {
        rend.material.color = hoverColor;   
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
