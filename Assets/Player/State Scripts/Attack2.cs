using System.Collections;
using System.Collections.Generic;
using UnityEngine;

  public class Attack2 : PlayerState {

    private bool attackPressed;
    void OnEnable() {
      initializeState("attack2");
      attackPressed = false;
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