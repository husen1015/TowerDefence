using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 20f;
    public GameObject impactEffect;
    public int damage;
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
        transform.LookAt(target);
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
        //create an impact effect and destroy the bullet and damage the enemy
        Destroy(gameObject);
        GameObject impact = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(impact,3f);
        target.gameObject.GetComponent<Enemy>().takeDamage(damage);//currently we only target enemies i.e game objs with enemy script
    }
}
