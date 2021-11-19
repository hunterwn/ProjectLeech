using System.Collections;
using System.Collections.Generic;
using UnityEngine;

  public class Attack3 : PlayerState {
    void OnEnable() {
      initializeState("attack3");
    }

    void Update() {
      if(CheckAnimationFinished())
      {
        EnterIdle();
      }
    }
  }