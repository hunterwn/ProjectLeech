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
    }

    void Update() {
      framewait++;

      if(framewait >= 250)
      {
        framewait = 0;
        
        //show death screen
        deathScreen.SetActive(true);


        Time.timeScale = 0;

        transform.position = player.current_checkpoint.transform.position;
        transform.rotation = player.current_checkpoint.transform.rotation;

        EnterIdle();

        player.movementDisabled = false;
        player.health = player.startingHealth;

        if(player.current_checkpoint.facing_direction < 0)
        {
          animator.Play("IdleL", -1, 0f);
        } else {
          animator.Play("IdleR", -1, 0f);
        }
      }
    }
  }