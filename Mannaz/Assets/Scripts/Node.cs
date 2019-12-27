using System.Collections;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    //private GameObject building;
    private Renderer rend;
    private Color startColor;
    private Vector3 shiftUp;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    void OnMouseDown()
    {
        GameObject turretToBuild = BuildManager.instance.GetBuildingToBuild();
        shiftUp = new Vector3(transform.localPosition.x, transform.localPosition.y + 2, transform.localPosition.z);
        GameObject turret = (GameObject)Instantiate(turretToBuild, shiftUp, transform.rotation);
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
