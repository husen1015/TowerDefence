using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    
    public static BuildManager Instance { get; private set; }
    public GameObject standardTurret;
    public GameObject MissileTurret;
    private GameObject turretToBuild;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("cannot have two instances of singleton class");
            return;
        }
        Instance = this;
    }


    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }
    public void setTurretToBuild(GameObject turretToBuild)
    {
        this.turretToBuild = turretToBuild;
    }
}
