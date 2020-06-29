using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected float health;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected int gems;

    [SerializeField]
    protected Transform pointA, pointB;

    private Vector3 _targetPosition;
    protected Animator anim;

    protected GameObject player;

    [SerializeField]
    protected GameObject diamondPrefab;

    public  virtual void Attack()
    {
        Debug.Log("Base attack called");
    }

    private void Start()
    {
        Init();   
    }

    public virtual void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            Move();
        }
        CheckAttack();
    }

    protected virtual void Init()
    {
        _targetPosition = pointA.position;
        anim = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player");

    }


    protected void CheckDirection()
    {
        float direction = transform.position.x - player.transform.position.x;
        float localScaleX = direction > 0 ? -Mathf.Abs(transform.localScale.x) : Mathf.Abs(transform.localScale.x);
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    public virtual void CheckAttack()
    {
        float d = Vector3.Distance(transform.position, player.transform.position);

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            CheckDirection();
        }


        if (Mathf.Abs(d) < 1f && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Hit") && player.GetComponent<Player>().isLife())
        {
            anim.SetBool("InCombat", true);

        }
        else
        {
            anim.SetBool("InCombat", false);
        }


    }

    protected void reward()
    {
        GameObject go = Instantiate(diamondPrefab, transform.position, Quaternion.identity);
        go.GetComponent<Diamond>().Gems += gems;
    }

    public virtual void Move()
    {
        if (Mathf.Approximately(Vector3.Distance(transform.position, _targetPosition), 0f))
        {
            anim.SetTrigger("Idle");
            _targetPosition = (_targetPosition == pointA.position ? pointB.position : pointA.position);
        }
        else
        {
            float localScaleX = _targetPosition == pointA.position ? -Mathf.Abs(transform.localScale.x) : Mathf.Abs(transform.localScale.x);
            transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
        }

        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);
    }

}
