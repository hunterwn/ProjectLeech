using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Player))]
public class PlayerInput : MonoBehaviour {

	Player player;

	void Start () {
		player = GetComponent<Player> ();
	}

	void Update () {
		Vector2 directionalInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		player.SetDirectionalInput (directionalInput);

		if (Input.GetKeyDown (KeyCode.Space)) {
			player.OnJumpInputDown ();
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			player.OnJumpInputUp ();
		}
		if(Input.GetKeyDown(KeyCode.K)) {
			player.OnAttackInputDown ();
		} else {
			player.attackInputDown = false;
		}
		if(Input.GetKeyUp(KeyCode.K)) {
			player.OnAttackInputUp ();
		} else {
			player.attackInputUp = false;
		}
		if(Input.GetKeyDown(KeyCode.LeftShift)) {
			player.OnRunInputDown();
		} else {
			player.runInputDown = false;
		}
		if(Input.GetKeyUp(KeyCode.LeftShift)) {
			player.OnRunInputUp();
		} else {
			player.runInputUp = false;
		}
		if(Input.GetKey(KeyCode.LeftShift))
		{
			player.runHeld = true;
		} else {
			player.runHeld = false;
		}
	}
}