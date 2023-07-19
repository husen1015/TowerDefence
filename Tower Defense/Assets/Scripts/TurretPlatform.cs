using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurretPlatform : MonoBehaviour
{
    public Color hoverColor;
    public Color noMoneyColor;
    Renderer platformRenderer;
    Color originalColor;

    private Vector3 positionOffset = new Vector3(0, 0.5f, 0);
    private BuildManager buildManager;
    public GameObject currTurret;
    void Start()
    {

        platformRenderer = GetComponent<Renderer>();
        originalColor = platformRenderer.material.color;
        buildManager = BuildManager.Instance;
    }
    private void OnMouseDown()
    {
        //Debug.Log( $"can build? {buildManager.CanBuild}");
        //Debug.Log($"curr turret? {currTurret}");

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
            }else
            {
                //build a turret
                buildManager.buildOn(this);
            }
        }
        else if (currTurret != null)// if user doesnt want to build a new turret i.e. selecting the platform instead if it has an existing turret
        {
            buildManager.SelectPlatform(this);
        }
    }

    
    private void OnMouseEnter()
    {

        //avoid UI elements that are in the way
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("ui blocking selection");
            return;
        }

        if (buildManager.CanBuild)
        {
            //TODO: put a preview here of the turret 
            if (!buildManager.hasMoney)
            {
                platformRenderer.material.color = noMoneyColor;

            }
            else
            {
                platformRenderer.material.color = hoverColor;
            }
        }
    }
    private void OnMouseExit()
    {

        platformRenderer.material.color = originalColor;
    }
    public Vector3 PositionOffset { get { return positionOffset; }   }
    public GameObject Turret { get { return currTurret; } set { currTurret = value; } }
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
}
