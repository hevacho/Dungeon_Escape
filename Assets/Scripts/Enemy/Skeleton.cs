using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Skeleton : Enemy, IDamageable
{
    public float Health { get; set; }

  
    
    public void Damage()
    {
        Health--;
        anim.SetTrigger("Hit");
        if (Health <= 0)
        {
            Destroy(gameObject);
        }

        CheckDirection();

    }

   

    protected override void Init()
    {
        base.Init();
        Health = base.health;
       
    }

   

    private void OnDrawGizmosSelected()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(pointA.position, pointB.position);
            Gizmos.DrawSphere(pointA.position, 0.15f);
            Gizmos.DrawSphere(pointB.position, 0.15f);
        }
    }
}
