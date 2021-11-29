using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {

	public float maxJumpHeight = 10;
	public float minJumpHeight = 5;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	float moveSpeed = 6;

	public Vector2 wallJumpClimb;
	public Vector2 wallJumpOff;
	public Vector2 wallLeap;
	public Vector2 addedVelocity;

	public float wallSlideSpeedMax = 3;
	public float wallStickTime = .25f;
	float timeToWallUnstick;
	public bool invincible;

	float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;

	public Controller2D controller;

	public Vector2 directionalInput;
	public bool wallSliding;
	public bool attackInputDown;
	public bool attackInputUp;
	int wallDirX;

	public Vector2 input_prev;

	void Start() {
		controller = GetComponent<Controller2D> ();

		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);

		GetComponent<Entry>().enabled = true;
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Backspace)) {
			transform.position = new Vector3(0, 0.5f, 0);
			transform.Rotate(-transform.rotation.eulerAngles);
		}
	}

	void FixedUpdate() {
		CalculateVelocity ();
		HandleWallSliding ();

		controller.Move (velocity * Time.deltaTime, directionalInput);
		
		if (controller.collisions.above || controller.collisions.below) {
			if (controller.collisions.slidingDownMaxSlope) {
				velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime;
			} else {
				velocity.y = 0;
			}
		}
	}

	public void SetDirectionalInput (Vector2 input) {
		input_prev = directionalInput;
		directionalInput = input;
	}

	public void OnJumpInputDown() {
		if (wallSliding) {
			if (wallDirX == directionalInput.x) {
				velocity.x = -wallDirX * wallJumpClimb.x;
				velocity.y = wallJumpClimb.y;
			}
			else if (directionalInput.x == 0) {
				velocity.x = -wallDirX * wallJumpOff.x;
				velocity.y = wallJumpOff.y;
			}
			else {
				velocity.x = -wallDirX * wallLeap.x;
				velocity.y = wallLeap.y;
			}
		}
		if (controller.collisions.below) {
			if (controller.collisions.slidingDownMaxSlope) {
				if (directionalInput.x != -Mathf.Sign (controller.collisions.slopeNormal.x)) { // not jumping against max slope
					velocity.y = maxJumpVelocity * controller.collisions.slopeNormal.y;
					velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x;
				}
			} else {
				velocity.y = maxJumpVelocity;
			}
		}
	}

	public void OnJumpInputUp() {
		if (velocity.y > minJumpVelocity) {
			velocity.y = minJumpVelocity;
		}
	}

	public void OnAttackInputDown() {
		attackInputDown = true;
	}

	public void OnAttackInputUp() {
		attackInputUp = true;
	}

	void HandleWallSliding() {
		wallDirX = (controller.collisions.left) ? -1 : 1;
		wallSliding = false;
		if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0) {
			wallSliding = true;

			if (velocity.y < -wallSlideSpeedMax) {
				velocity.y = -wallSlideSpeedMax;
			}

			if (timeToWallUnstick > 0) {
				velocityXSmoothing = 0;
				velocity.x = 0;

				if (directionalInput.x != wallDirX && directionalInput.x != 0) {
					timeToWallUnstick -= Time.deltaTime;
				}
				else {
					timeToWallUnstick = wallStickTime;
				}
			}
			else {
				timeToWallUnstick = wallStickTime;
			}

		}

	}

	void CalculateVelocity() {
		float targetVelocityX = directionalInput.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;

		velocity.x += addedVelocity.x;
		velocity.y += addedVelocity.y;

		addedVelocity.x = 0;
		addedVelocity.y = 0;
	}

     public IEnumerator DamageFlash(Color flashColor, float flashTime, float flashSpeed)
     {
		SkinnedMeshRenderer renderer = GameObject.Find("Character").GetComponent<SkinnedMeshRenderer>();
		Material mat = renderer.material;
		Color originalColor = mat.color;
		Color c = originalColor;
		c.a = 0.5f;
		flashColor.a = 0.5f;
		
		renderer.material = null;
		renderer.material.color = flashColor;

		float flashingFor = 0.0f;
		int alternator = 0;
		while(flashingFor < flashTime)
		{
			yield return new WaitForSeconds(flashSpeed);
			if(alternator % 2 == 0)
			{
				renderer.material = mat;
				renderer.material.color = c;
			} else {
				renderer.material = null;
				renderer.material.color = flashColor;
			}
			alternator += 1;
			flashingFor += flashSpeed;
		}

		renderer.material = mat;
		renderer.material.color = originalColor;

		invincible = false;        
     }
}