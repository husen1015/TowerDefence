using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    
    public static BuildManager Instance { get; private set; }
    public GameObject standardTurret;
    private GameObject turretToBuild;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("cannot have two instances of singleton class");
            return;
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        turretToBuild = standardTurret;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }
}
