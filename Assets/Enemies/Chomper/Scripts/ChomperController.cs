using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ChomperController : Enemy
{
    public Transform[] points;
    private int destPoint = 0;
    private bool brake = false;
    private ChomperState animController;
    public NavMeshAgent agent;
    private Animator animator;
    public HitboxController hitboxController;
    public ChomperDamageController damageController;
    public ChomperFOVCone FOVCone;
    public ChomperState state;
    public float minWalkVelocity = 1.0f;
    private Vector3 previousPosition;
    public float velocity = 0.0f;
    public bool damaged = false;

    //sfx
    public AudioSource damage1SFX;
    public AudioSource damage2SFX;
    public AudioSource deathSFX;
    public int attackTimer;
    public int attackCooldown = 400;
    public int hp = 3;
    void Start () {
        this.agent = GetComponent<NavMeshAgent>();
        this.animController = GetComponent<ChomperState>();
        this.animator = animController.animator;
        this.hitboxController = GetComponent<HitboxController>();
        this.hitboxController = GetComponent<HitboxController>();
        this.damageController = transform.Find("hurtbox").GetComponent<ChomperDamageController>();
        this.FOVCone = GetComponent<ChomperFOVCone>();

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

    public override IEnumerator StunEnemy(float frames)
    {
        this.stunned = true;
        this.FOVCone.enabled = false;
        this.agent.isStopped = true;
        if(!dead){
            this.animator.Play("Chomper_Hit1", -1, 0f);
        }
        yield return new WaitForSeconds(frames);
        if(!dead)
        {
            this.stunned = false;
            this.agent.isStopped = false;
            this.FOVCone.enabled = true;
        }
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
