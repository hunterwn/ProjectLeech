using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlide : PlayerState {
  void OnEnable() {
    initializeState("wallslide");
  }
  void Update() {
    if(player.controller.collisions.below)
    {
      EnterLanding();
      return;
    }

    if((Input.GetKeyDown (KeyCode.Space)) ||
        (!player.controller.collisions.left && !player.controller.collisions.right))
    {
        EnterFall();
        return;
    }
  }
}