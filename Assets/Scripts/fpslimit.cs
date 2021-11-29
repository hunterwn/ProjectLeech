using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fpslimit : MonoBehaviour
{

    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        coroutine = FPSlim(0);
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FPSlim(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
}
