using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }
    public GameObject standardTorret;

    GameObject turretToBuild;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one BuildManager are runnig!");
            return;
        }
        Instance = this;
    }

    void Start()
    {
        turretToBuild = standardTorret;
    }

    void Update()
    {

    }

    public GameObject GetTorretToBuild()
    {
        return turretToBuild;
    }
}
