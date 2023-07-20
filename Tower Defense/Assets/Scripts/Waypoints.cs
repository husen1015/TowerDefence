using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] waypointsArr;
    void Awake()
    {
        waypointsArr = new Transform[transform.childCount]; // init the waypoints array 
        for (int i = 0; i < waypointsArr.Length; i++)
        {
            waypointsArr[i] = transform.GetChild(i);
        }
    }


}
