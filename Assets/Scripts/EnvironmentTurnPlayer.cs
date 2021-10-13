using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentTurnPlayer : MonoBehaviour {
  enum Direction { left, right };
  [SerializeField]
  Direction turnDirection = Direction.right;

  [SerializeField]
  Transform turnStartPos;
  [SerializeField]
  Transform turnEndPos;

  [SerializeField]
  private GameObject objToTurn;
  private CharacterController playerController;

  private Vector3 center;
  private float radius;
  private float[] rotSnapPoints = {0f, 30f, 45f, 60f, 90f};

  // Start is called before the first frame update
  void Start() {
    var requiredAttributes = new[] {turnStartPos, turnEndPos};

    foreach (var item in requiredAttributes) {
      if (item == null) {
        Debug.LogError("Define attributes for " + this.GetType().ToString());
        UnityEditor.EditorApplication.isPlaying = false;
      }
    }

    playerController = objToTurn.GetComponent<CharacterController>();

    center = new Vector3(turnEndPos.position.x, turnStartPos.position.y, turnStartPos.position.z);
    radius = Mathf.Abs(turnEndPos.position.z - turnStartPos.position.z);
  }

  private void OnTriggerEnter(Collider other) {
    if (other.gameObject == objToTurn) {
      Debug.Log(snapAngle(objToTurn.transform.rotation.eulerAngles.y));
      objToTurn.transform.eulerAngles = Vector3.up * snapAngle(objToTurn.transform.rotation.eulerAngles.y);
    }
  }

  private void OnTriggerStay(Collider other) {
    if (other.gameObject == objToTurn) {
      objToTurn = other.gameObject;

      Vector3 preferredPlayerPos = playerPositionBetweenPoints();
      playerController.Move(preferredPlayerPos - objToTurn.transform.position);

      float newPlayerAngle = playerAngleBetweenPoints();
      objToTurn.transform.eulerAngles = Vector3.up * (turnStartPos.rotation.eulerAngles.y + newPlayerAngle);
    }
  }

  private void OnTriggerExit(Collider other) {
    if (other.gameObject == objToTurn) {
      objToTurn.transform.eulerAngles = Vector3.up * snapAngle(objToTurn.transform.rotation.eulerAngles.y);
    }
  }

  private float snapAngle(float angle) {
    float closestAngle = rotSnapPoints[0];
    float smallestDist = Mathf.Abs(rotSnapPoints[0] - angle);
    for (int i = 1; i < rotSnapPoints.Length; i++) {
      float dist = Mathf.Abs(angle - rotSnapPoints[i]);
      if (smallestDist > dist) {
        smallestDist = dist;
        closestAngle = Mathf.Sign(angle) * rotSnapPoints[i];
      }
    }

    return closestAngle; 
  }

  private Vector3 playerPositionBetweenPoints() {
    Vector3 p1 = objToTurn.transform.position - center;
    Vector2 closestPoint = closestPointToCircle(new Vector2(p1.x, p1.z));

    Vector3 preferredPos = new Vector3(closestPoint.x, objToTurn.transform.position.y, closestPoint.y);
    return preferredPos + center;
  }

  private Vector2 closestPointToCircle(Vector2 p1) {
    float R = radius;
    float x = Mathf.Sign(p1.x) * Mathf.Sqrt((R*R) / (1 + Mathf.Pow(p1.y / p1.x, 2)));
    float y = Mathf.Sign(p1.y) * Mathf.Sqrt(Mathf.Pow(R, 2) - Mathf.Pow(x, 2));
    return new Vector2(x, y);
  }

  private float playerAngleBetweenPoints() {
    // Radius of turn is determined by the x
    float R = radius;
    float x = objToTurn.transform.position.x - center.x;

    // Handle asymptotes of derivative
    float delta = 0.1f;
    if (Mathf.Abs(x) > R - delta && Mathf.Abs(x) < R + delta) {
      return Mathf.Sign(x) * -90;
    }

    float derivative = -x / Mathf.Sqrt(R*R - x*x);
    float angle = Mathf.Atan(derivative) * 180 / Mathf.PI;
    return angle;
  }
}
