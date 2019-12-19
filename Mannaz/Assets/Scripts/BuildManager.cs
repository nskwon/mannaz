using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene");
            return;
        }
        instance = this;

    }
    public GameObject standardTBuildingPrefab;

    void Start()
    {
        buildingToBuild = standardTBuildingPrefab;
    }
    private GameObject buildingToBuild;

    public GameObject GetBuildingToBuild()
    {
        return buildingToBuild;
    }
}
