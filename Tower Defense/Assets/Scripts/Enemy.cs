using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Attributes")]
    public float Startinghealth = 4;
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
    // Start is called before the first frame update
    void Start()
    {
        speed = topSpeed;
        currHealth = Startinghealth;
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
        speed= topSpeed;//unclear how this is working currently
    }
    public void takeDamage(float amount)
    {
        currHealth -= amount;
        healthBar.fillAmount = currHealth / Startinghealth;
        //destroy enemy and reward money
        if(currHealth <= 0)
        {
            Destroy(gameObject);
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
            currTarget = Waypoints.waypointsArr[++wayPointIndex];
        }
        else
        {
            Destroy(gameObject);
            gameManager.incrementLives(-1);
            
        }
    }
    
}
