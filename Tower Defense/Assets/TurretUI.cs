using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurretUI : MonoBehaviour
{
    public GameObject ui;
    private TurretPlatform platform;
    public TextMeshProUGUI upgradeText;
    public TextMeshProUGUI sellText;

    private void Start()
    {

    }
    public void SetTarget(TurretPlatform plat)
    {
        platform = plat;
        transform.position = platform.GetBuildPosition();
        ui.SetActive(true);
        upgradeText.text = $"Upgrade\n{plat.currTurretBlueprint.upgradeCost.ToString()}";
        sellText.text = plat.IsUpgraded ? $"Sell\n{plat.currTurretBlueprint.sellPriceU.ToString()}" : $"Sell\n{plat.currTurretBlueprint.sellPrice.ToString()}";


    }
    public void Hide()
    {
        ui.SetActive(false);
    }
    public void Upgrade()
    {
        //upgrade
        platform.UpgradeTurret();
        //update sell price
        sellText.text = $"Sell\n{platform.currTurretBlueprint.sellPriceU.ToString()}";

    }
}
