using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    public int facing_direction;

    public Player player;
    [SerializeField]
    public GameObject canvas;

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Player")
        {
            this.player = collision.gameObject.GetComponent<Player>();

            if(player.current_checkpoint != this)
            {
                player.current_checkpoint = this;
                StartCoroutine(DisplayText());
            }
        }
    }

    IEnumerator DisplayText()
    {
        canvas.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        canvas.SetActive(false);
    }
}