using System.Collections;
using System.Collections.Generic;
using UnityEngine;



  public class Attack3 : PlayerState {

    public float moveAmount = 0.1f;
    private Vector2 move;

    private int faceDir;
    void OnEnable() {
      initializeState("attack3");
      faceDir = GetFacingDirection();
      move.x = moveAmount * faceDir;
      move.y = -0.01f;

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

    void FixedUpdate() {
      controller.Move(move, false);
      move.x *= 0.9f;
    }

    void Update() {
      if(CheckAnimationFinished())
      {
        EnterIdle();
      }
    }
  }