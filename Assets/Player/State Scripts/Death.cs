using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

  public class Death : PlayerState {

    public GameObject deathScreen;
    int framewait = 0;
    void OnEnable() {
      initializeState("death");
      player.movementDisabled = true;
      player.dead = true;
    }

    void Update() {
      framewait++;

      if(framewait >= 250)
      {
        framewait = 0;

        player.freezePosition = true;
        player.movementDisabled = true;

        //show death screen
        deathScreen.SetActive(true);
      }
    }
  }