using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurretPlatform : MonoBehaviour
{
    public Color hoverColor;
    Renderer platformRenderer;
    Color originalColor;

    private Vector3 positionOffset;
    private BuildManager buildManager;
    private GameObject currTurret;
    void Start()
    {
        platformRenderer= GetComponent<Renderer>();
        originalColor = platformRenderer.material.color;
        buildManager = BuildManager.Instance;
    }
    private void OnMouseDown()
    {
        //avoid UI elements that are in the way
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        } 
        if (buildManager.CanBuild)
        {
            if (currTurret != null)
            {
                Debug.Log("slot already occupied");
            }
            else
            {
                //build a turret
                buildManager.buildOn(this);

            }
        }
    }
    private void OnMouseEnter()
    {
        //avoid UI elements that are in the way
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (buildManager.CanBuild)
        {
            platformRenderer.material.color = hoverColor;
        }
    }
    private void OnMouseExit()
    {
        platformRenderer.material.color = originalColor;

    }
    public Vector3 PositionOffset { get { return positionOffset; }   }
    public GameObject Turret { get { return currTurret; } set { } }
}
