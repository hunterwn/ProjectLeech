using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Boss boss;

    // Update is called once per frame
    void Update()
    {
        if(boss.dead)
        {
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }
}
