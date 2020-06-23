using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamageable
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




}
