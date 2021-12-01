using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChomperController : MonoBehaviour
{
    public Transform[] points;
    private int destPoint = 0;
    public NavMeshAgent agent;
    public bool brake = false;
    public ChomperState animController;
    public HitboxController hitboxController;
    public ChomperDamageController damageController;
    public ChomperFOVCone FOVCone;
    public ChomperState state;
    public float minWalkVelocity = 1.0f;
    private Vector3 previousPosition;
    public float velocity;
    public bool damaged;
    public bool dead;

    //sfx
    public AudioSource damage1SFX;
    public AudioSource damage2SFX;
    public AudioSource deathSFX;
    public int attackTimer;
    public int attackCooldown = 400;
    public int hp = 3;
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        animController = GetComponent<ChomperState>();
        hitboxController = GetComponent<HitboxController>();
        hitboxController = GetComponent<HitboxController>();
        damageController = transform.Find("hurtbox").GetComponent<ChomperDamageController>();
        FOVCone = GetComponent<ChomperFOVCone>();
        velocity = 0.0f;
        damaged = false;
        dead = false;

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = brake;

        GotoNextPoint();
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
