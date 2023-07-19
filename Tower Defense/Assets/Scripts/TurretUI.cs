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

    public void SetTarget(TurretPlatform plat)
    {
        platform = plat;
        transform.position = platform.GetBuildPosition();
        ui.SetActive(true);
        upgradeText.text = $"Upgrade\n{plat.currTurretBlueprint.upgradeCost.ToString()}";
        sellText.text = plat.IsUpgraded ? $"Sell\n{plat.currTurretBlueprint.sellPriceU.ToString()}$" : $"Sell\n{plat.currTurretBlueprint.sellPrice.ToString()}$";


    }
    public void Hide()
    {
        ui.SetActive(false);
    }
    public void Upgrade()
    {
        if (!platform.IsUpgraded)
        {
            //upgrade
            platform.UpgradeTurret();
            //update sell price
            sellText.text = $"Sell\n{platform.currTurretBlueprint.sellPriceU.ToString()}$";
            upgradeText.text = $"MAX";

        }

    }
    public void Sell()
    {
        platform.SellTurret();
        //might want to use buildManager deselect platform
        Hide();
    }
}
