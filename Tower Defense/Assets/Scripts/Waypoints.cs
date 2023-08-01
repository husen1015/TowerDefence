using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] waypointsArr;
    public static List<List<Transform>> waypointsList; // list of paths each path with its waypoints
    void Awake()
    {
        waypointsList = new List<List<Transform>>(transform.childCount);

        //init each path
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform currPath = transform.GetChild(i);
            int numOfWaypoints = currPath.childCount;

            waypointsList.Add(new List<Transform>(currPath.childCount));
            //init each waypoint in the current path
            for (int j = 0; j < numOfWaypoints; j++)
            {
                waypointsList[i].Add(currPath.GetChild(j));
            }
        }
        //waypointsArr = new Transform[transform.childCount]; // init the waypoints array 
        //for (int i = 0; i < waypointsArr.Length; i++)
        //{
        //    waypointsArr[i] = transform.GetChild(i);
        //}
    }


}
