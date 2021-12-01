using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideFromCamera : MonoBehaviour {
  [SerializeField]
  private float threshold = 20f;
  private int camIgnoreLayer;

  private void Start() {
    this.camIgnoreLayer = LayerMask.NameToLayer("CameraIgnore");
  }

  private void Update() {
    Vector3 v1 = Camera.main.transform.forward;
    Vector3 v2 = gameObject.transform.parent.forward;
    float angle = Vector2.SignedAngle(new Vector2(v1.x, v1.z), new Vector2(v2.x, v2.z));
    
    if (angle < 90f+this.threshold && angle > 90f-this.threshold) {
      gameObject.layer = this.camIgnoreLayer;
    }
    else {
      gameObject.layer = 0;
    }
  }
}
