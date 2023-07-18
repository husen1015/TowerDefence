using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;
    private int balance;
    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.Instance;
    }
    public void SelectStandardTurret()
    {
        Debug.Log("standard turret selected");
        buildManager.setTurretToBuild(buildManager.standardTurret);
    }
    public void SelectMissileTurret()
    {
        Debug.Log("Missile turret selected");
        buildManager.setTurretToBuild(buildManager.MissileTurret);
    }
}
