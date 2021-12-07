using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    
    private GameObject deathScreen;
    [SerializeField]
    private Player player;

    void OnEnable() {
        this.deathScreen = transform.parent.transform.parent.gameObject;
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    public void doRespawn()
    {
        Time.timeScale = 1;
        deathScreen.SetActive(false);
        this.player.Respawn();
        AudioListener.pause = false;
    }

    public void doExitGame()
    {
        Application.Quit();
    }
}
