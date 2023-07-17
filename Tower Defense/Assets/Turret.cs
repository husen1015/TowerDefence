using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform partToRotate;
    private Transform currTarget = null;
    private float range = 15f;
    private float rotationSpeed = 8f;
    // Start is called before the first frame update
    void Start()
    {
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
            //rotate the turret to point at the closest target
            Vector3 dir = (transform.position - currTarget.position).normalized;
            Quaternion lookRotatation = Quaternion.LookRotation(dir);
            //using quaternion.lerp to make a smooth transition when retargeting instead of snapping to a new target
            Vector3 eulerRotation = Quaternion.Lerp(partToRotate.rotation, lookRotatation, Time.deltaTime * rotationSpeed).eulerAngles;
            partToRotate.rotation = Quaternion.Euler(0f, eulerRotation.y, 0f);
        }
    }
    //draw a gizmos to visualize turret range in the scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
