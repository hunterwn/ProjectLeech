using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour {
  private enum TurretType { Timer, Raycast };

  [SerializeField]
  private TurretType type;
  [SerializeField]
  private GameObject laser;
  [SerializeField]
  private GameObject laserBeam;
  [SerializeField]
  private GameObject aimLaser;
  [SerializeField]
  private GameObject laserLight;
  [SerializeField]
  private LayerMask laserLayerMask;
  [SerializeField]
  private float lightGapDistance = 2.5f;
  [SerializeField]
  private float windUpTime = 2.5f;
  [SerializeField]
  private float shootDuration = 2f;
  [SerializeField]
  private float rechargeDuration = 2f;
  [SerializeField]
  private float aimLaserWidth = 0.02f;

  private bool turretDown;
  private float secondsDown;
  private Coroutine shootCoroutine;
  private AudioSource soundEffect;
  private Light chargingLight;
  private float endingLightIntensity;
  private LineRenderer aimLaserRenderer;
  private float aimLaserCurWidth;
  private bool isShooting = false;

  private GameObject[] lights = new GameObject[50];

  private void Start() {
    this.soundEffect = GetComponent<AudioSource>();
    this.chargingLight = this.laserLight.GetComponent<Light>();
    this.aimLaserRenderer = this.aimLaser.GetComponent<LineRenderer>();
    this.endingLightIntensity = this.chargingLight.intensity;
    this.chargingLight.intensity = 0f;
    this.aimLaserCurWidth = 0f;

    SetupLaser();

    if (this.type == TurretType.Timer) {
      this.shootCoroutine = StartCoroutine(ShootLaser());
    }
  }

  private void Update() {
    // Check if player is in view of the turret
    if (this.type == TurretType.Raycast && !this.isShooting) {
      RaycastHit hit;
      Ray laserRay = new Ray(this.laser.transform.position, transform.forward);
      Physics.Raycast(laserRay, out hit, laserLayerMask.value);
      if (hit.collider.tag == "Player") {
        this.shootCoroutine = StartCoroutine(ShootLaser());
      }
    }

    // Charge up animation
    if (this.laserLight.activeSelf) {
      // Aiming Laser
      this.aimLaserCurWidth += Time.deltaTime * this.aimLaserWidth / this.windUpTime;
      this.aimLaserCurWidth = Mathf.Clamp(this.aimLaserCurWidth, 0, this.aimLaserWidth);
      this.aimLaserRenderer.widthCurve = AnimationCurve.Constant(0, 1, this.aimLaserCurWidth);

      // Charging Light
      float lightIntensity = this.chargingLight.intensity;
      lightIntensity += Time.deltaTime * this.endingLightIntensity / this.windUpTime;
      lightIntensity = Mathf.Clamp(lightIntensity, 0, endingLightIntensity);
      this.chargingLight.intensity = lightIntensity;
    }

    // Check if Turret is disabled
    if (this.turretDown) {
      // Reset Coroutine
      StopCoroutine(this.shootCoroutine);
      this.shootCoroutine = StartCoroutine(ShootLaser());
    }
  }

  public void DisableTurret(float seconds) {
    this.secondsDown = seconds;
    this.turretDown = true;

    this.soundEffect.Stop();
    this.laser.SetActive(false);
    this.laserLight.SetActive(false);
    this.aimLaser.SetActive(false);
    this.aimLaserCurWidth = 0;
  }

  private void SetupLaser() {
    RaycastHit hit;
    Ray laserRay = new Ray(this.laser.transform.position, transform.forward);
    Physics.Raycast(laserRay, out hit, laserLayerMask.value);
    float laserLength = hit.distance;

    this.laserBeam.transform.localScale = Vector3.forward * laserLength;
    this.aimLaser.transform.localScale = Vector3.forward * laserLength;
    int numLights = Mathf.RoundToInt(laserLength / lightGapDistance);
    for (int i = 0; i < lights.Length; i++) {
      if (i >= numLights) {
        GameObject.Destroy(lights[i]);
        continue;
      }

      if (lights[i] == null) {
        lights[i] = GameObject.Instantiate(this.laserLight, this.laser.transform);
        lights[i].transform.position = this.laser.transform.position + transform.forward * this.lightGapDistance * i;
        lights[i].SetActive(true);
      }
    }
  }

  IEnumerator ShootLaser() {
    while (true) {
      this.isShooting = true;

      // Check if Turret is disabled
      if (this.turretDown) {
        this.turretDown = false;
        yield return new WaitForSeconds(this.secondsDown);
        continue;
      }

      // Laser wind up
      this.soundEffect.Play();
      this.laserLight.SetActive(true);
      this.aimLaser.SetActive(true);
      this.chargingLight.intensity = 0f;
      this.aimLaserCurWidth = 0f;
      yield return new WaitForSeconds(this.windUpTime);
      this.aimLaser.SetActive(false);
      this.laserLight.SetActive(false);

      // Laser Fire
      this.laser.SetActive(true);
      yield return new WaitForSeconds(this.shootDuration);

      // Laser Off
      this.soundEffect.Stop();
      this.laser.SetActive(false);

      yield return new WaitForSeconds(this.rechargeDuration);

      this.isShooting = false;
      if(this.type != TurretType.Timer) {
        break;
      }
    }
  }
}
