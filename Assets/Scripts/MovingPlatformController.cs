using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour {
  [SerializeField]
  private Vector3 moveDistance = new Vector3(10f, 0f, 0f);
  [SerializeField]
  private float timeToEnd = 5f;
  
  private Vector3 startPos;
  private float elapsedTime;

  private void Start() {
    this.startPos = transform.position;
    this.elapsedTime = 0f;
  }

  private void Update() {
    this.elapsedTime += Time.deltaTime;
    float percentPos = this.elapsedTime / this.timeToEnd;

    if (this.elapsedTime <= this.timeToEnd) {
      transform.position = Vector3.Lerp(startPos, startPos + moveDistance, percentPos);
    }
    else if (this.elapsedTime <= this.timeToEnd * 2) {
      transform.position = Vector3.Lerp(startPos + moveDistance, startPos, percentPos-1);
    }
    else {
      this.elapsedTime = 0f;
    }
  }
}
