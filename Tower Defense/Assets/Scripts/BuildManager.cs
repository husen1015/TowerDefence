using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BuildManager : MonoBehaviour
{
    
    public static BuildManager Instance { get; private set; }
    public GameObject buildEffect;
    public TurretUI turretUI;
    //public GameObject standardTurret;
    //public GameObject MissileTurret;
    private TurretBlueprint turretToBuild;
    
    private TurretPlatform selectedPlat;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("cannot have two instances of singleton class");
            return;
        }
        Instance = this;
        turretToBuild = null;
    }
    public TurretBlueprint CurrTurr { get { return turretToBuild; } }
    public bool CanBuild { get {return turretToBuild!=null && turretToBuild.prefab!= null; } } //getter property
    public bool hasMoney { get { return GameManager.Balance > turretToBuild.cost; } } //getter property


    public void setTurretToBuild(TurretBlueprint turretToBuild)
    {
        Debug.Log($"setting turret {turretToBuild} with cost {turretToBuild.cost} and prefab {turretToBuild.prefab}");

        this.turretToBuild = turretToBuild;
        turretUI.Hide();
        //selectedPlat = null;
    }
    public void SelectPlatform(TurretPlatform plat)
    {
        Debug.Log(this.selectedPlat);
        Debug.Log(this.selectedPlat != null && plat == this.selectedPlat);
        //if pressing the same platform twice hide ui
        if (plat == this.selectedPlat)
        {
            DeselectPlatform();
            return;
        }
        this.selectedPlat = plat;
        turretUI.SetTarget(plat);
        //turretToBuild = null;
    }
    public void DeselectPlatform()
    {
        this.selectedPlat = null;
        turretUI.Hide();
    }
    public void unselectTurret()
    {
        this.turretToBuild = null;
        //this.turretToBuild.prefab = null;
        //this.turretToBuild.cost = -1;

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
            GameObject effect = Instantiate(buildEffect, turretPlatform.transform.position, Quaternion.identity);
            Destroy(effect, 3f);
            turretPlatform.Turret = turr;
            //GameManager.Balance -= turretToBuild.cost;
            GameManager.Instance.incrementBalance(-1 * turretToBuild.cost);
            Debug.Log($"building, money left: {GameManager.Balance}");

        }
    }
}
