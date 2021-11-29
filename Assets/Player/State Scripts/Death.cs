using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

  public class Death : PlayerState {
    int framewait = 0;
    void OnEnable() {
      initializeState("death");
      player.freezePosition = true;
    }

    void Update() {
      framewait++;

      if(framewait >= 250)
      {
        //show death screen
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      }
    }
  }