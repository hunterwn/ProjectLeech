using System.Collections.Generic;
using UnityEngine;

public class DroneState : MonoBehaviour
{
    public string animid;
    public Animator animator;
    public HitboxController hitboxController;
    public DroneController droneController;
    public int currentFrame;

    public void initializeState(string animid) 
    {
        this.animid = animid;
        this.animator = GetComponent<Animator>();
        this.hitboxController = GetComponent<HitboxController>();
        this.droneController = GetComponent<DroneController>();
        this.currentFrame = 0;
        animator.SetBool(this.animid, true);
        droneController.state = this;
    }

    public virtual void OnTakeDamage()
    {
        
        if (droneController.hp == 1)
        {
            EnterHit1();
        }

        droneController.hp = (droneController.hp == 1) ? 3 : droneController.hp - 1;
    }

    public bool CheckAnimationFinished() {
      return (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0));
    }

    public bool CheckAnimationTransition() {
      AnimatorTransitionInfo currentTransition = animator.GetAnimatorTransitionInfo(0);
      return (currentTransition.duration > 0);
    }
    public void EnterIdle() {
      this.enabled = false;
      animator.SetBool(this.animid, false);
      GetComponent<DroneIdle>().enabled = true;
    }

    public void EnterWalk() {
      this.enabled = false;
      animator.SetBool(this.animid, false);
      GetComponent<DroneMove>().enabled = true;
    }

    public void EnterAttack() {
      this.enabled = false;
      animator.SetBool(this.animid, false);
      GetComponent<DroneAttack>().enabled = true;
    }

    public void EnterHit1() {
      this.enabled = false;
      animator.SetBool(this.animid, false);
      GetComponent<DroneHit1>().enabled = true;
    }
}