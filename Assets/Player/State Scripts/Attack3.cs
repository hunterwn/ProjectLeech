using System.Collections;
using System.Collections.Generic;
using UnityEngine;



  public class Attack3 : PlayerState {

    public float moveAmount = 10.0f;
    private int faceDir;
    void OnEnable() {
      initializeState("attack3");
      faceDir = GetFacingDirection();
      player.addedVelocity.x = 10.0f * faceDir;
      if(faceDir == 1)
      {
        hitboxController.CreateHitbox("foot.R", 4);
      } else {
        hitboxController.CreateHitbox("foot.L", 4);
      }
    }

    void OnDisable() {
      hitboxController.ClearHitboxes();
    }

    void Update() {
      if(CheckAnimationFinished())
      {
        EnterIdle();
      }
    }
  }