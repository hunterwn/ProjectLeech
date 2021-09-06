using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
  public class Example : PlayerState {
    void OnEnable() {
      initializeState("example");
    }
    void Update() {
      PhysicsHandler();
      CollisionHandler();
      InputHandler();
    }

    void PhysicsHandler() {

    }

    void CollisionHandler() {

    }

    void InputHandler() {

    }
  }
}