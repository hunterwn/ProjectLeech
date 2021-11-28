using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int numOfBars;

    public Image[] bars;
    public Sprite filledBar;
    public Sprite emptyBar;

    void Update()
    {
        if (health > numOfBars)
            health = numOfBars;
        for (int i = 0; i < bars.Length; i++)
        {
            if (i < health)
                bars[i].sprite = filledBar;
            else
                bars[i].sprite = emptyBar;

            if (i < numOfBars)
                bars[i].enabled = true;
            else
                bars[i].enabled = false;
        }
    }
}
