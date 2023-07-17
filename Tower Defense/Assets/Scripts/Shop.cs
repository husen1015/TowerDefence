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

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectStandardTurret()
    {
        Debug.Log("standard turret purchased");
        buildManager.setTurretToBuild(buildManager.standardTurret);
    }
    public void SelectMissileTurret()
    {
        Debug.Log("Missile turret purchased");
        buildManager.setTurretToBuild(buildManager.MissileTurret);
    }
}
