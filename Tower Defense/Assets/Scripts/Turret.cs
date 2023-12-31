using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using static UnityEngine.GraphicsBuffer;

public class Turret : MonoBehaviour
{
    private Transform currTarget = null;
    private Enemy currEnemyScript;
    [Header("Attributes")]
    public float range = 15f;
    public Transform partToRotate;
    public Transform firePoint;
    public string firingSound;

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
    public int DamageOverTime = 10;
    public float slowingFactor = 0.3f;
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
            currEnemyScript = currTarget.GetComponent<Enemy>();
        }
        else
        {
            //if using laser then stop slowing enemy
            if(useLaser && currTarget != null)
            {
                currEnemyScript.ResetSpeed();
                AudioManager.Instance.StopLaserSound();
            }
            
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
        AudioManager.Instance.PlayShootingSound(firingSound);
        GameObject currBullet = (GameObject)Instantiate(Bullet, firePoint.transform.position, firePoint.transform.rotation);
        Bullet bulletScript = currBullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.fire(currTarget);
        }
    }
    private void ShootLaser()
    {
        AudioManager.Instance.PlayLaserShot();
        currEnemyScript.takeDamage(DamageOverTime * Time.deltaTime);//currently we only target enemies i.e game objs with enemy script
        currEnemyScript.Slow(slowingFactor);
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
