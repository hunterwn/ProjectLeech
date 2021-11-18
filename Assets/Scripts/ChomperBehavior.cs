using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperBehavior : MonoBehaviour
{

    public meleeAttack meleeAttack;

    public ParticleSystem hitParticlePrefab;
    public LayerMask targetLayers;

    [Tooltip("Time in seconds before the Chomper stops pursuing the player out of sight")]
    public float timeToStopPursuit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
