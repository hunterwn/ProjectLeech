using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public PlayerDamageController pdc;
    public int health;
     //public int currentHealth;

    public Image[] bars;
    public Sprite filledBar;
    public Sprite emptyBar;

    void Update()
    {
        if (health > pdc.player.health)
            health = pdc.player.health;

        for (int i = 0; i < bars.Length; i++)
        {
            if (i < pdc.player.maxHealth)
            {
                bars[i].sprite = filledBar;
            }
            else
            {
                bars[i].sprite = emptyBar;
            }

            if (i < pdc.player.health)
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
