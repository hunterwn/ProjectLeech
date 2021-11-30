using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : PlayerState {
  void OnEnable() {
    initializeState("fall");
  }
  void Update() {
    if(player.attackInputDown)
    {
      EnterAirKick();
      return;
    }
    if (player.controller.collisions.below) {
      EnterLanding();
      return;
    }
    if(player.controller.collisions.left || player.controller.collisions.right)
    {
      EnterWallSlide();
      return;
    }
    
  }
}