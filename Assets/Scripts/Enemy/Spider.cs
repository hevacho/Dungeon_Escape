using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    public float Health { get; set; }

    protected override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public void Damage()
    {
        Destroy(gameObject);
    }

    public override void CheckAttack()
    {
        float d = Vector3.Distance(transform.position, player.transform.position);

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            CheckDirection();
        }


        if (Mathf.Abs(d) < 5f && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            anim.SetBool("InCombat", true);

        }
        else
        {
            anim.SetBool("InCombat", false);
        }


    }
}
