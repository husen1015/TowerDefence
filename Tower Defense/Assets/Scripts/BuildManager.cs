using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BuildManager : MonoBehaviour
{
    
    public static BuildManager Instance { get; private set; }
    //public GameObject standardTurret;
    //public GameObject MissileTurret;
    private TurretBlueprint turretToBuild;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("cannot have two instances of singleton class");
            return;
        }
        Instance = this;
    }
    public bool CanBuild { get {return turretToBuild!= null; } } //getter property

    //public TurretBlueprint GetTurretToBuild()
    //{
    //    return turretToBuild;
    //}
    public void setTurretToBuild(TurretBlueprint turretToBuild)
    {
        this.turretToBuild = turretToBuild;
    }

    public void buildOn(TurretPlatform turretPlatform)
    {
        if (GameManager.Balance < turretToBuild.cost)
        {
            Debug.Log("not enough money");
        }
        else
        {
            GameObject turr = Instantiate(turretToBuild.prefab, turretPlatform.transform.position + turretPlatform.PositionOffset, Quaternion.identity);
            turretPlatform.Turret = turr;
            GameManager.Balance -= turretToBuild.cost;
            Debug.Log($"building, money left: {GameManager.Balance}");
        }
    }
}
