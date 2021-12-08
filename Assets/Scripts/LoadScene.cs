using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField]
    public string sceneName;
    private void OnTriggerEnter(Collider coll) {
        if(coll.tag == "Player")
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
