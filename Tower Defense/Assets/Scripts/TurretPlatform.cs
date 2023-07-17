using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurretPlatform : MonoBehaviour
{
    public Vector3 positionOffset;
    public Color hoverColor;
    Renderer platformRenderer;
    Color originalColor;

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
        if (!(buildManager.GetTurretToBuild() == null))
        {
            if (currTurret != null)
            {
                Debug.Log("slot already occupied");
            }
            else
            {
                //build a turret
                GameObject turret = buildManager.GetTurretToBuild();
                currTurret = Instantiate(turret, transform.position + positionOffset, transform.rotation);
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
        if (!(buildManager.GetTurretToBuild() == null))
        {
            platformRenderer.material.color = hoverColor;
        }
    }
    private void OnMouseExit()
    {
        platformRenderer.material.color = originalColor;

    }
}
