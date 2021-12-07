using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class DeathScreen : MonoBehaviour
{
    public AudioMixer audioMixer;
    private GameObject deathScreen;
    [SerializeField]
    private Player player;

    void OnEnable() {
        this.deathScreen = transform.parent.transform.parent.gameObject;
        Time.timeScale = 0;
        audioMixer.SetFloat("SoundEffectVolume", -80.0f);
        AudioManager am = AudioManager.instance;
        am.stop("theme");
        am.play("death");
    }

    public void doRespawn()
    {
        Time.timeScale = 1;
        audioMixer.SetFloat("SoundEffectVolume", 0.0f);
        deathScreen.SetActive(false);
        this.player.Respawn();

    }

    public void doExitGame()
    {
        Application.Quit();
    }
}
