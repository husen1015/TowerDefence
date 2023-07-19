using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint 
{
    public int cost;
    public GameObject prefab;

    public GameObject upgradedPrefab;
    public int upgradeCost;

    public int sellPrice;
    public int sellPriceU;
}
