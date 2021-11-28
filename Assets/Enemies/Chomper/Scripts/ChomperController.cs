using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChomperController : MonoBehaviour
{
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    public bool brake = false;
    public ChomperState animController;
    public ChomperState state;
    public float minWalkVelocity = 1.0f;
    private Vector3 previousPosition;
    public float velocity;
    public bool damaged;

    public int attackTimer;
    public int attackCooldown = 400;

    void Start () {
        agent = GetComponent<NavMeshAgent>();
        animController = GetComponent<ChomperState>();
        previousPosition = transform.position;
        velocity = 0.0f;
        damaged = false;

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = brake;

        GotoNextPoint();
    }

    public void OnTakeDamage()
    {
        damaged = true;
        Debug.Log("take damage");
        state.EnterHit1();
    }

    void GotoNextPoint() {
        // Returns if no points have been set up
        if (points.Length == 0)
        {
            return;
        }

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }


    void Update () {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }
        Vector3 curMove = transform.position - previousPosition;
        velocity = curMove.magnitude / Time.deltaTime;
        previousPosition = transform.position;

        attackTimer++;
    }
}