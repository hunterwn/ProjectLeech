using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrb : MonoBehaviour
{
    public int healAmount = 1;
    public Player player;

    private void OnTriggerEnter(Collider collider) {
        if(collider.gameObject.tag == "Player")
        {
            this.player = collider.gameObject.GetComponent<Player>();
            if(this.player.health + healAmount <= this.player.maxHealth)
            {
                this.player.health += healAmount;
            } else {
                this.player.health = this.player.maxHealth;
            }
            
            gameObject.SetActive(false);
        }
    }
}