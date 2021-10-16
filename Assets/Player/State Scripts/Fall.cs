using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : PlayerState {
  void OnEnable() {
    initializeState("fall");
  }
  void Update() {
    if (player.controller.collisions.below) {
      EnterLanding();
    } else if (player.directionalInput.x == -1 * player.input_prev.x)
    {
      SetFacingDirection(animator.GetInteger("facing_direction") * -1);
      EnterFall();
    }
  }
}