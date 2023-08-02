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
    public int pathId;

    private float currHealth;
    private float speed;
    private Transform currTarget;
    private int wayPointIndex = 0;
    private int waypointsNum;
    private GameManager gameManager;
    private bool isDead;
    private Animator animator;
    private float velocity;
    private bool isSlowing = false;

    //rotation related variables
    private bool isRotating = false;
    private Quaternion targetRotation;
    private float rotationSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        this.pathId = WaveSpawner.currPathIndx;
        speed = topSpeed;
        currHealth = StartingHealth;
        //currTarget = Waypoints.waypointsArr[0];
        currTarget = Waypoints.waypointsList[this.pathId][0]; //set the first waypoint of the current path
        transform.LookAt(currTarget);
        //waypointsNum = Waypoints.waypointsArr.Length;
        waypointsNum = Waypoints.waypointsList[this.pathId].Count;

        gameManager = GameManager.Instance;
        isDead= false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = currTarget.position - this.transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, currTarget.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
        speed = topSpeed;//unclear how this is working currently
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
    public void ResetSpeed()
    {
        if (animator != null)
        {
            isSlowing = false;
            StartCoroutine(decreaseVelocity());
        }
    }
    public void Slow(float amount)
    {
        //slow the speed as well as the blend tree in the animator
        isSlowing = true; 
        speed = topSpeed * (1 - amount);
        if (animator != null)
        {
            velocity = animator.GetFloat("Velocity");
            if (velocity < 1)
            {
                StartCoroutine(increaseVelocity());
            }
        }
    }
    private IEnumerator increaseVelocity()
    {
        while (velocity < 1 && isSlowing)
        {
            velocity += Time.deltaTime * 0.01f;
            animator.SetFloat("Velocity", velocity);
            yield return null;
        }
    }
    private IEnumerator decreaseVelocity()
    {
        while (velocity > 0)
        {
            velocity -= Time.deltaTime * 0.5f;
            animator.SetFloat("Velocity", velocity);
            yield return null;
        }
    }
    private void GetNextWaypoint()
    {
        if (wayPointIndex < waypointsNum - 1) 
        {
            currTarget = Waypoints.waypointsList[this.pathId][++wayPointIndex];

            //rotate towards the next waypoint

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
