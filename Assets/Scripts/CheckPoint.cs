using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    public int facing_direction;

    public Player player;

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Player")
        {
            this.player = collision.gameObject.GetComponent<Player>();
            player.current_checkpoint = this;
        }
    }
}