using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public GameObject deathScreen;

    void OnEnable() {
        this.deathScreen = transform.parent.transform.parent.gameObject;
    }

    public void doRespawn()
    {
        Time.timeScale = 1;

        deathScreen.SetActive(false);
    }

    public void doExitGame()
    {
        Application.Quit();
    }
}
