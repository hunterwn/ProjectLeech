using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField]
    public string sceneName;
    
    private enum LoadType { swap, add };
    [SerializeField]
    private LoadType loadType = LoadType.swap;

    private void OnTriggerEnter(Collider coll) {
        if(coll.tag == "Player")
        {
            if (loadType == LoadType.swap) {
                SceneManager.LoadScene(sceneName);
            }
            else if (loadType == LoadType.add) {
                SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            }
        }
    }
}
