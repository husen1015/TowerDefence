using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int Balance;
    public int startBalance = 400;
    BuildManager buildManager;
    private void Start()
    {
        buildManager = BuildManager.Instance;
        Balance = startBalance;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            buildManager.unselectTurret();
        }
    }
}
