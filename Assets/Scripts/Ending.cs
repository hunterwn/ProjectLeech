using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Ending : MonoBehaviour
{
    private GameObject endingScreen;
    //[SerializeField]
    //private Player player;


    public void goToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void doExitGame()
    {
        Application.Quit();
    }
}
