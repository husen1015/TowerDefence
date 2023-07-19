using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint StandardTurret;
    public TurretBlueprint MissileTurret;
    public TurretBlueprint LaserTurret;

    BuildManager buildManager;
    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.Instance;
    }
    public void SelectStandardTurret()
    {
        Debug.Log($"standard turret selected building {StandardTurret}");
        buildManager.setTurretToBuild(StandardTurret);
    }
    public void SelectMissileTurret()
    {
        Debug.Log("Missile turret selected");
        buildManager.setTurretToBuild(MissileTurret);
    }
    public void SelectLaserTurret()
    {
        Debug.Log("laser turret selected");
        buildManager.setTurretToBuild(LaserTurret);
    }
}
