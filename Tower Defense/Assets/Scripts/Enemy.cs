using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    private Transform currTarget;
    private int wayPointIndex = 0;
    private int waypointsNum;
    // Start is called before the first frame update
    void Start()
    {
        currTarget = Waypoints.waypointsArr[0];
        waypointsNum = Waypoints.waypointsArr.Length;
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

    private void GetNextWaypoint()
    {
        if (wayPointIndex < waypointsNum - 1) 
        {
            currTarget = Waypoints.waypointsArr[++wayPointIndex];
        }
        else
        {
            Destroy(gameObject);
            //TODO - end game OR remove a life 
        }
    }
}
