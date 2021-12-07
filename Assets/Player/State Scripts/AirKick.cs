using System.Collections;
using System.Collections.Generic;
using UnityEngine;


  public class AirKick : PlayerState {

    void OnEnable() {
      initializeState("airkick");
      int faceDir = GetFacingDirection();
      if(faceDir == 1)
      {
        hitboxController.CreateHitbox("foot.L", 4);
      } else {
        hitboxController.CreateHitbox("foot.R", 4);
      }
      hitboxController.CreateHitbox("hip", 4);
    }

    void OnDisable() {
      hitboxController.ClearHitboxes();
    }

    void Update() {
      if (CheckAnimationFinished()) {
        EnterFall();
      }
      if (player.controller.collisions.below) {
        EnterLanding();
        return;
      }
    }
  }