using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    float speed = 20f;
    public GameObject impactEffect;
    public void fire(Transform target)
    {
        this.target = target;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if target got destroyed
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 dir = target.position - transform.position; //distance between target and bullet
        float distanceOverFrame = speed * Time.deltaTime; // distance covered in the curr frame
        if(dir.magnitude <= distanceOverFrame ) // if hit
        {
            HitTarget();
        }
        else
        {
            transform.Translate(dir.normalized * distanceOverFrame, Space.World);
        }

    }

    private void HitTarget()
    {
        Destroy(gameObject);
        GameObject impact = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(impact,2f);
        Destroy(target.gameObject);
        Debug.Log("HIT"); 
    }
}
