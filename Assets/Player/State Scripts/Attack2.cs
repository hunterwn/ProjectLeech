using System.Collections;
using System.Collections.Generic;
using UnityEngine;

  public class Attack2 : PlayerState {

    private int faceDir;

    private bool attackPressed;
    void OnEnable() {
      initializeState("attack2");
      faceDir = GetFacingDirection();
      attackPressed = false;
      if(faceDir == 1)
      {
        hitboxController.CreateHitbox("hand.L", 4);
      } else {
        hitboxController.CreateHitbox("hand.R", 4);
      }
    }

    void OnDisable() {
      hitboxController.ClearHitboxes();
    }

    void Update() {
      if(CheckAnimationFinished())
      {
        if(attackPressed)
        {
          EnterAttack3();
        } else {
          EnterIdle();
        }
      }
      if(player.attackInputDown)
      {
        attackPressed = true;
      }
    }
  }