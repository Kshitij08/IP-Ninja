using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{

    private IEnemyState currentState;

    public GameObject Target { get; set; }

    [SerializeField]
    private float meleeRange;

    [SerializeField]
    private float throwRange;

    public bool InMeleeRange
    {
        get
        {
            if(Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange;
            }

            return false;
        }
    }

    public bool InThrowRange
    {
        get
        {
            if (Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= throwRange;
            }

            return false;
        }
    }

    public override bool IsDead
    {
        get
        {
            return health <= 0;
        }
    }

    private Vector2 startPos;

    [SerializeField]
    private Transform leftEdge;

    [SerializeField]
    private Transform rightEdge;


    // Use this for initialization
    public override	void Start () {

        base.Start();
        startPos = transform.position;
        Player.Instance.Dead += new DeadEventHandler(RemoveTarget);
        ChangeState(new IdleState());
	}
	
	// Update is called once per frame
	void Update () {

        if (!IsDead)
        {
            if (!TakingDamage)
            {
                currentState.Execute();
            }

            LookAtTarget();
        } 
	}

    public void RemoveTarget()
    {
        Target = null;
        ChangeState(new PatrolState());
    }

    private void LookAtTarget()
    {
        if(Target != null)
        {
            float xDir = Target.transform.position.x - transform.position.x;

            if(xDir < 0 && facingRight || xDir > 0 && !facingRight)
            {
                ChangeDirection();
            }
        }
    }

    public void ChangeState(IEnemyState newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);

    }

    public void Move()
    {
        if (!Attack)
        {
            if((GetDirection().x > 0 && transform.position.x < rightEdge.position.x) || (GetDirection().x < 0 && transform.position.x > leftEdge.position.x))
            {
                MyAnimator.SetFloat("speed", 1);

                transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
            }
        }
        else if(currentState is PatrolState){
            ChangeDirection();
        }
        //else if (currentState is RangedState)
        //{
        //    Target = null;
        //    ChangeState(new IdleState());
        //}


    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        currentState.OnTriggerEnter(other);
    }

    public override IEnumerator TakeDamage()
    {
        health -= 10;

        if (!IsDead)
        {
            MyAnimator.SetTrigger("damage");
        }
        else
        {
            MyAnimator.SetTrigger("die");
            yield return null;
        }
    }

    public override void Death()
    {
        //To respawn the enemy
        MyAnimator.ResetTrigger("die");
        MyAnimator.SetTrigger("idle");

        
        Player.score += 20;
        GameControl.control.coins += 20;
        //To respawn 
        //transform.position = startPos;
        //health = 10;


        //To destroy the enemy
        Destroy(gameObject);
    }
}
