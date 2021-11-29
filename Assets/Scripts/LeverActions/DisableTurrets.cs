using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTurrets : MonoBehaviour, LeverAction {
  [SerializeField]
  private float disableTime = 5f;
  [SerializeField]
  private TurretController[] turretsToDisable;

  public void ExecuteAction() {
    foreach (TurretController turretController in this.turretsToDisable) {
      turretController.DisableTurret(this.disableTime);
    }
  }
}
