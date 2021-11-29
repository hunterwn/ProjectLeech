using UnityEngine;
using UnityEngine.AI;

[ExecuteAlways]
public class EnvironmentTurnPlayer : MonoBehaviour {
  [SerializeField]
  bool showHelpers = false;
  [SerializeField]
  Transform turnStartPos;
  [SerializeField]
  Transform turnEndPos;
  enum Direction {
    Left,
    Right
  }
  [SerializeField]
  private Direction turnDir = Direction.Right;
  private GameObject objToTurn;

  private Vector3 center;
  private float radius;
  private float snapPosInterval = 0.25f;
  private float snapAngleInterval = 15f;
  private bool objInTrigger = false;

  // Helpers
  GameObject centerHelper;
  GameObject circleHelper;

  void Start() {
    initializeTurnCircle();
  }

  private void initializeTurnCircle() {
    float centerX;
    float centerY;
    if (this.turnDir == Direction.Right) {
      centerX = turnEndPos.position.x;
      centerY = turnStartPos.position.z;
    }
    else {
      centerX = turnStartPos.position.x;
      centerY = turnEndPos.position.z;
    }
    this.center = new Vector3(centerX, turnStartPos.position.y, centerY);
    this.radius = Mathf.Abs(turnEndPos.position.z - turnStartPos.position.z);
  }

  private void Update() {
    if (this.showHelpers) {
      initializeTurnCircle();
      if (centerHelper == null && circleHelper == null) {
        this.centerHelper = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        this.centerHelper.transform.position = this.center;

        this.circleHelper = new GameObject { name = "Circle" };
        this.circleHelper.transform.position = this.center + Vector3.up*0.1f;
        this.circleHelper.DrawCircle(this.radius, .1f);
      }
    }
    else {
      GameObject.DestroyImmediate(this.centerHelper);
      GameObject.DestroyImmediate(this.circleHelper);
    }
  }

  private void FixedUpdate() {
    if (objInTrigger) {
      Vector3 preferredPlayerPos = playerPositionBetweenPoints();
      objToTurn.transform.position = preferredPlayerPos;

      float newPlayerAngle = playerAngleBetweenPoints(preferredPlayerPos);
      float sign = this.turnDir == Direction.Right ? -1 : 1;
      objToTurn.transform.eulerAngles = Vector3.up * (newPlayerAngle + 180f);
    }
  }

  private void OnTriggerEnter(Collider other) {
    if (other.tag == "Player") {
      objToTurn = other.gameObject;
    }
    
    if (other.gameObject == objToTurn) {
		  objInTrigger = true;
    }
  }

  private void OnTriggerExit(Collider other) {
    if (other.gameObject == objToTurn) {
      objInTrigger = false;
      objToTurn.transform.eulerAngles = Vector3.up * snapAngle(objToTurn.transform.rotation.eulerAngles.y);
      objToTurn.transform.position = snapPosition(objToTurn.transform.position);
    }
  }

  private Vector3 snapPosition(Vector3 pos) {
    float snappedPosX = Mathf.Round(pos.x / this.snapPosInterval) * this.snapPosInterval;
    float snappedPosZ = Mathf.Round(pos.z / this.snapPosInterval) * this.snapPosInterval;
    return new Vector3(snappedPosX, pos.y, snappedPosZ);
  }

  private float snapAngle(float angle) {
    float snappedAngle = Mathf.Round(angle / this.snapAngleInterval) * this.snapAngleInterval;
    return snappedAngle;
  }

  private Vector3 playerPositionBetweenPoints() {
    Vector3 p1 = objToTurn.transform.position - center;
    Vector2 closestPoint = closestPointToCircle(new Vector2(p1.x, p1.z));

    Vector3 preferredPos = new Vector3(closestPoint.x, objToTurn.transform.position.y, closestPoint.y);
    return preferredPos + new Vector3(center.x, 0, center.z);
  }

  private Vector2 closestPointToCircle(Vector2 p1) {
    float R = this.radius;
    float x = Mathf.Sign(p1.x) * Mathf.Sqrt((R*R) / (1 + Mathf.Pow(p1.y / p1.x, 2)));
    float y = Mathf.Sign(p1.y) * Mathf.Sqrt(Mathf.Pow(R, 2) - Mathf.Pow(x, 2));
    
    return new Vector2(x, y);
  }

  private float playerAngleBetweenPoints(Vector3 preferredPlayerPos) {
    // Radius of turn is determined by the x
    float R = this.radius;
    float x = preferredPlayerPos.x - center.x;
    float y = preferredPlayerPos.z - center.z;

    // Handle asymptotes of derivative
    // float delta = 0.001f;
    // if (Mathf.Abs(x) > R - delta && Mathf.Abs(x) < R + delta) {
    //   return Mathf.Sign(x) * -90;
    // }

    float derivative = -x / Mathf.Sqrt(R*R - x*x);
    float angle = 90f - Mathf.Atan(derivative) * 180 / Mathf.PI;
    if (y < 0f) {
      angle = 360 - angle;
    }
    return angle;
  }
}
