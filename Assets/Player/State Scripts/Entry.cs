using System.Collections;
using System.Collections.Generic;
using UnityEngine;
  public class Entry : PlayerState {
    void OnEnable() {
      initializeState("entry");

      EnterIdle(); //TODO: make entry animation and remove this line
    }
  }