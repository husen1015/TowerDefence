using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Attributes")]
    public int health = 4;
    public float speed = 10f;
    public int worth = 25;
    public GameObject deathEffect;

    private Transform currTarget;
    private int wayPointIndex = 0;
    private int waypointsNum;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        currTarget = Waypoints.waypointsArr[0];
        waypointsNum = Waypoints.waypointsArr.Length;
        gameManager = GameManager.Instance;
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
    }
    public void takeDamage(int amount)
    {
        this.health -= amount;
        Debug.Log(this.health);
        //destroy enemy and reward money
        if(health <= 0)
        {
            Destroy(gameObject);
            GameObject effect = Instantiate(deathEffect, this.transform.position, Quaternion.identity);
            Destroy(effect, 3f);

            gameManager.incrementBalance(worth);
        }
    }
    private void GetNextWaypoint()
    {
        if (wayPointIndex < waypointsNum - 1) 
        {
            currTarget = Waypoints.waypointsArr[++wayPointIndex];
        }
        else
        {
            Destroy(gameObject);
            gameManager.incrementLives(-1);
            
        }
    }
}
