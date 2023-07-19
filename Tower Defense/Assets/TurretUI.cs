using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUI : MonoBehaviour
{
    public GameObject ui;
    private TurretPlatform platform;
    private void Start()
    {

    }
    public void SetTarget(TurretPlatform plat)
    {
        platform = plat;
        transform.position = platform.GetBuildPosition();
        ui.SetActive(true);
        
    }
    public void Hide()
    {
        ui.SetActive(false);
    }
}
