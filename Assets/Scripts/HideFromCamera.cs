using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideFromCamera : MonoBehaviour {
  [SerializeField]
  private float threshold = 45f;
  private int camIgnoreLayer;
  private Renderer rendr;

  private void Start() {
    this.camIgnoreLayer = LayerMask.NameToLayer("CameraIgnore");
    this.rendr = GetComponentInChildren<Renderer>();
  }

  private void Update() {
    Vector3 v1 = Camera.main.transform.forward;
    Vector3 v2 = gameObject.transform.right;
    float angle = Vector2.SignedAngle(new Vector2(v1.x, v1.z), new Vector2(v2.x, v2.z));

    Debug.DrawLine(this.rendr.bounds.center, this.rendr.bounds.center + v2 * 2);

    if (angle < this.threshold && angle > -this.threshold) {
      this.rendr.enabled = false;
    }
    else {
      this.rendr.enabled = true;
    }
  }
}
