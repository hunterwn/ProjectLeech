using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public bool dead = false;
    
    [HideInInspector]
    public bool stunned = false;
    public void Stun(float frames)
    {
        StartCoroutine(this.StunEnemy(frames));
    }

    public virtual IEnumerator StunEnemy(float frames)
    {
        yield return new WaitForSeconds(frames);
    }
}
