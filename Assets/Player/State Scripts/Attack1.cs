using System.Collections;
using System.Collections.Generic;
using UnityEngine;


  public class Attack1 : PlayerState {

    private bool attackPressed;
    void OnEnable() {
      initializeState("attack1");
      attackPressed = false;
    }

    void Update() {
      if(CheckAnimationFinished())
      {
        if(attackPressed)
        {
          EnterAttack2();
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