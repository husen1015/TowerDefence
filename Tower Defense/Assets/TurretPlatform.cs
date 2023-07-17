using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlatform : MonoBehaviour
{
    public Vector3 positionOffset;
    public Color hoverColor;
    Renderer platformRenderer;
    Color originalColor;

    private GameObject currTurret;
    void Start()
    {
        platformRenderer= GetComponent<Renderer>();
        originalColor = platformRenderer.material.color;
    }
    private void OnMouseDown()
    {
        if(currTurret != null)
        {
            Debug.Log("slot already occupied");
        }
        else
        {
            //build a turret
            GameObject turret = BuildManager.Instance.GetTurretToBuild();
            currTurret = Instantiate(turret, transform.position + positionOffset, transform.rotation);
        }
    }
    private void OnMouseEnter()
    {
        platformRenderer.material.color = hoverColor;
    }
    private void OnMouseExit()
    {
        platformRenderer.material.color = originalColor;

    }
}
