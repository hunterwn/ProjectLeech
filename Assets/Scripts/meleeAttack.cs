using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeAttack : MonoBehaviour
{
    public int damage = 1;

    [System.Serializable]
    public class AttackPoint
    {
        public float radius;
        public Vector3 offset;
        public Transform attackRoot;
    }

    public ParticleSystem hitParticlePrefab;
    public LayerMask targetLayers;

    public AttackPoint[] attackPoints = new AttackPoint[0];

    public TimeEffect[] effects;

    [Header("Audio")] public RandomAudioPlayer hitAudio;
    public RandomAudioPlayer attackAudio;

    public bool throwingHit
    {
        get { return IsThrowingHit; }
        set { IsThrowingHit = value; }
    }

    protected GameObject Owner;

    protected Vector3[] PreviousPos = null;
    protected Vector3 Direction;

    protected bool IsThrowingHit = false;
    protected bool InAttack = false;

    const int PARTICLE_COUNT = 10;
    protected ParticleSystem[] ParticlesPool = new ParticleSystem[PARTICLE_COUNT];
    protected int CurrentParticle = 0;

    protected static RaycastHit[] RaycastHitCache = new RaycastHit[32];
    protected static Collider[] ColliderCache = new Collider[32];

    private void Awake()
    {
        if (hitParticlePrefab != null)
        {
            for (int i = 0; i < PARTICLE_COUNT; ++i)
            {
                ParticlesPool[i] = Instantiate(hitParticlePrefab);
                ParticlesPool[i].Stop();
            }
        }
    }

    public void SetOwner(GameObject owner)
    {
        this.Owner = owner;
    }

    public void BeginAttack(bool throwingAttack)
    {
        throwingHit = throwingAttack;

        this.InAttack = true;

        this.PreviousPos = new Vector3[attackPoints.Length];

        for (int i = 0; i < attackPoints.Length; ++i)
        {
            Vector3 worldPos = attackPoints[i].attackRoot.position +
                                attackPoints[i].attackRoot.TransformVector(attackPoints[i].offset);
            this.PreviousPos[i] = worldPos;
        }
    }

    public void EndAttack()
    {
        InAttack = false;
    }

    private void FixedUpdate()
    {
        if (this.InAttack)
        {
            for (int i = 0; i < attackPoints.Length; ++i)
            {
                AttackPoint pts = attackPoints[i];

                Vector3 worldPos = pts.attackRoot.position + pts.attackRoot.TransformVector(pts.offset);
                Vector3 attackVector = worldPos - this.PreviousPos[i];

                if (attackVector.magnitude < 0.001f)
                {
                    // A zero vector for the sphere cast don't yield any result, even if a collider overlap the "sphere" created by radius. 
                    // so we set a very tiny microscopic forward cast to be sure it will catch anything overlaping that "stationary" sphere cast
                    attackVector = Vector3.forward * 0.0001f;
                }


                Ray r = new Ray(worldPos, attackVector.normalized);

                int contacts = Physics.SphereCastNonAlloc(r, pts.radius, RaycastHitCache, attackVector.magnitude,
                    ~0,
                    QueryTriggerInteraction.Ignore);

                for (int k = 0; k < contacts; ++k)
                {
                    Collider col = RaycastHitCache[k].collider;

                    if (col != null)
                    {
                        //CheckDamage(col, pts);
                    }
                }

                PreviousPos[i] = worldPos;
            }
        }
    }

    

    
}
