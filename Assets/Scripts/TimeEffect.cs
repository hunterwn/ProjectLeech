using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEffect : MonoBehaviour
{
    public Light staffLight;
    
    Animation Animation;

    void Awake()
    {
        this.Animation = GetComponent<Animation>();

        gameObject.SetActive(false);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        staffLight.enabled = true;

        if (this.Animation)
            this.Animation.Play();

        StartCoroutine(DisableAtEndOfAnimation());
    }

    IEnumerator DisableAtEndOfAnimation()
    {
        yield return new WaitForSeconds(this.Animation.clip.length);

        gameObject.SetActive(false);
        staffLight.enabled = false;
    }
} 