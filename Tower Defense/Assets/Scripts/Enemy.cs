using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Attributes")]
    public float StartingHealth = 4;
    public float topSpeed = 10f;
    public int worth = 25;
    public GameObject deathEffect;
    public Image healthBar;

    private float currHealth;
    private float speed;
    private Transform currTarget;
    private int wayPointIndex = 0;
    private int waypointsNum;
    private GameManager gameManager;
    private bool isDead;
    public int pathId;

    //rotation related variables
    private bool isRotating = false;
    private Quaternion targetRotation;
    private float rotationSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        this.pathId = WaveSpawner.currPathIndx;
        speed = topSpeed;
        currHealth = StartingHealth;
        //currTarget = Waypoints.waypointsArr[0];
        currTarget = Waypoints.waypointsList[this.pathId][0]; //set the first waypoint of the current path
        //waypointsNum = Waypoints.waypointsArr.Length;
        waypointsNum = Waypoints.waypointsList[this.pathId].Count;
        Debug.Log(waypointsNum);

        gameManager = GameManager.Instance;
        isDead= false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currTarget.position);
        Vector3 dir = currTarget.position - this.transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, currTarget.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
        speed= topSpeed;//unclear how this is working currently
    }
    public void takeDamage(float amount)
    {
        currHealth -= amount;
        healthBar.fillAmount = currHealth / StartingHealth;
        //destroy enemy and reward money
        if(currHealth <= 0)
        {
            //Destroy(gameObject);
            die();
            GameObject effect = Instantiate(deathEffect, this.transform.position, Quaternion.identity);
            Destroy(effect, 3f);

            gameManager.incrementBalance(worth);
        }
    }
    public void Slow(float amount)
    {
        speed = topSpeed * (1 - amount);
    }
    private void GetNextWaypoint()
    {
        if (wayPointIndex < waypointsNum - 1) 
        {
            currTarget = Waypoints.waypointsList[this.pathId][++wayPointIndex];

            //rotate towards the next waypoint
            //this makes a sudden turn which is not relaistic enough
            //transform.LookAt(currTarget); 

            // Calculate the rotation direction to the next waypoint
            Vector3 directionToTarget = currTarget.position - transform.position;
            targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);

            // start rotating using a coroutine
            if (!isRotating)
            {
                StartCoroutine(RotateTowardsTarget());
            }
        }
        else
        {
            //Destroy(gameObject);
            die();
            gameManager.incrementLives(-1);
            
        }
    }

    private IEnumerator RotateTowardsTarget()
    {
        isRotating = true;

        while (transform.rotation != targetRotation)
        {
            // Gradually rotate towards the target rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            yield return null;
        }

        isRotating = false;
    }
    private void die()
    {
        if (!isDead)
        {
            Destroy(gameObject);
            WaveSpawner.ActiveEnemies -= 1;
            isDead = true;
        }
    }
    //public void setPath(int pathId)
    //{
    //    currTarget = Waypoints.waypointsList[pathId][0];
    //    waypointsNum = Waypoints.waypointsList[pathId].Count;
    //    this.pathId = pathId;
    //}

}
