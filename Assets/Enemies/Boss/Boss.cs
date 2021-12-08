using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Boss : Enemy
{
    public float minimumWalkSpeed = 2.0f;
    public float walkFastSpeed = 5.0f;
    public NavMeshAgent agent;
    public Animator animator;

    [HideInInspector]
    public float velocity;

    [HideInInspector]
    public Vector3 previousPosition;
    [HideInInspector]
    public Vector3 target_position;
    //States
    public State current_state;
    public State idle;
    public State walkSlow;
    public State walkFast;

    private int numAttacks = 3;
    public State attack1;
    public State attack2;
    public State attack3;

    private void InitializeStates()
    {
        this.idle = new State("idle", () => IdleCallback());
        this.walkSlow = new State("walkSlow", () => WalkSlowCallback());
        this.walkFast = new State("walkFast", () => WalkFastCallback());
        this.attack1 = new State("attack1", () => AttackCallback());
        this.attack2 = new State("attack2", () => AttackCallback());
        this.attack3 = new State("attack3", () => AttackCallback());
    }
    //Unity functions
    private void Start() {
        InitializeStates();

        //Enter initial state
        EnterState(idle);
    }

    private void Update() {
        CalculateVelocity();

        //execute the callback in the current state
        current_state.ExecuteCallback();
    }
    private void OnTriggerEnter(Collider coll) {
        if(coll.tag == "Player")
        {
            if(!agent.isStopped)
            {
                agent.isStopped = true;

                //Use a random attack animation
                System.Random rand = new System.Random();
                int randomAttackType = rand.Next(1, numAttacks);
                switch(randomAttackType)
                {
                    case 1:
                        EnterState(attack1);
                        break;
                    case 2:
                        EnterState(attack2);
                        break;
                    case 3:
                        EnterState(attack3);
                        break;
                }
            }
        }
    }

    //Methods
    public void EnterState(State state)
    {
        current_state = state;
        animator.SetTrigger(state.animid);
    }
    public bool CheckAnimationFinished() {
        return (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0));
    }
    private void CalculateVelocity()
    {
        Vector3 curMove = transform.position - previousPosition;
        velocity = curMove.magnitude / Time.deltaTime;
        previousPosition = transform.position;
    }

    //State Callbacks
    private void IdleCallback()
    {
        if(velocity > walkFastSpeed)
        {
            EnterState(walkFast);
        } else if (velocity > minimumWalkSpeed)
        {
            EnterState(walkSlow);
        }
    }

    private void WalkSlowCallback()
    {
        if(velocity < minimumWalkSpeed)
        {
            EnterState(idle);
        } else if (velocity > walkFastSpeed)
        {
            EnterState(walkFast);
        }
    }

    private void WalkFastCallback()
    {
        if(velocity < minimumWalkSpeed)
        {
            EnterState(idle);
        } else if (velocity < walkFastSpeed)
        {
            EnterState(walkSlow);
        }
    }

    private void AttackCallback()
    {
        if(CheckAnimationFinished())
        {
            agent.isStopped = false;

            EnterState(idle);
        }
    }
}