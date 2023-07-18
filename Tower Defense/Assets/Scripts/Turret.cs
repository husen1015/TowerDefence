using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Turret : MonoBehaviour
{
    private Transform currTarget = null;
    [Header("Attributes")]
    private float range = 15f;
    public Transform partToRotate;
    public Transform firePoint;

    [Header("Use Bullets(default)")]
    public GameObject Bullet;
    public float fireRate = 1f;
    private float rotationSpeed = 8f;
    private float fireCountDown = 0f;

    [Header("Use Laser")]
    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem LaserImpact;
    public Light impactLight;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer= GetComponent<LineRenderer>();//can be null
        InvokeRepeating("UpdateTarget", 0f, 0.5f); //look for a target every 2 seconds to avoid performance issues

    }

    void UpdateTarget() 
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); //find all enemies in the scene
        float nearestEnemyDist = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach(GameObject enemy in enemies){ //find nearest enemy
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < nearestEnemyDist)
            {
                nearestEnemyDist = distanceToEnemy;
                nearestEnemy = enemy;
            }

        }
        if(nearestEnemy != null && nearestEnemyDist <= range) 
        {
            currTarget = nearestEnemy.transform;
        }
        else
        {
            currTarget = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currTarget != null)
        {
            LockOnTarget();
            //should shoot
            if (!useLaser)
            {
                if (fireCountDown <= 0)
                {
                    Shoot();
                    fireCountDown = 1f / fireRate;
                }
                fireCountDown -= Time.deltaTime;
            }else
            {

                ShootLaser();
            }
        }
        else
        {
            //if we dont have a target we dont want any lasers
            if(useLaser && lineRenderer != null) 
            {
                lineRenderer.enabled = false;
                LaserImpact.Stop();
                impactLight.enabled= false;
            }
        }

    }

    private void Shoot()
    {
        //Debug.Log("shoot");
        GameObject currBullet = (GameObject)Instantiate(Bullet, firePoint.transform.position, firePoint.transform.rotation);
        Bullet bulletScript = currBullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.fire(currTarget);
        }
    }
    private void ShootLaser()
    {
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            LaserImpact.Play();
            impactLight.enabled=true;
        }
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, currTarget.position);

        Vector3 dir = (firePoint.position - currTarget.position);
        LaserImpact.transform.position = currTarget.position + dir.normalized;
        LaserImpact.transform.rotation = Quaternion.LookRotation(dir) ; 
    }
    void LockOnTarget()
    {
        //rotate the turret to point at the closest target
        Vector3 dir = (transform.position - currTarget.position).normalized;
        Quaternion lookRotatation = Quaternion.LookRotation(dir);
        //using quaternion.lerp to make a smooth transition when retargeting instead of snapping to a new target
        Vector3 eulerRotation = Quaternion.Lerp(partToRotate.rotation, lookRotatation, Time.deltaTime * rotationSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, eulerRotation.y, 0f);
    }
    //draw a gizmos to visualize turret range in the scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
