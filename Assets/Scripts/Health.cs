using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int currentHealth;

    public Image[] bars;
    public Sprite filledBar;
    public Sprite emptyBar;

    void Update()
    {
        if (health > currentHealth)
            health = currentHealth;

        for (int i = 0; i < bars.Length; i++)
        {
            if (i < health)
            {
                bars[i].sprite = filledBar;
            }
            else
            {
                bars[i].sprite = emptyBar;
            }

            if (i < currentHealth)
            {
                bars[i].enabled = true;
            }
            else
            {
                bars[i].enabled = false;

            }

        }
    }
}
