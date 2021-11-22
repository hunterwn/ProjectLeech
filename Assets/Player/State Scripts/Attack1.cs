using System.Collections;
using System.Collections.Generic;
using UnityEngine;


  public class Attack1 : PlayerState {

    private bool attackPressed;


    public int endFrame = 30;
    public int interruptFrame = 20;
    private int faceDir;

    void OnEnable() {
      initializeState("attack1");
      attackPressed = false;
      this.currentFrame = 0;
      faceDir = GetFacingDirection();
      if(faceDir == 1)
      {
        hitboxController.CreateHitbox("hand.R", 4);
      } else {
        hitboxController.CreateHitbox("hand.L", 4);
      }
    }

    void OnDisable() {
      hitboxController.ClearHitboxes();
    }

    void Update() {
      this.currentFrame++;

      if(currentFrame >= endFrame)
      {
          EnterIdle();
      }
      if(player.attackInputDown)
      {
        attackPressed = true;
      }
      if(attackPressed && currentFrame > interruptFrame)
      {
        EnterAttack2();
      }
    }
  }