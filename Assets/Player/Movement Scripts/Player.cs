using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {
	public float maxJumpHeight = 10;
	public float minJumpHeight = 5;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	public float moveSpeed = 6;
	public float runSpeed = 10;
	public float wallSlideSpeedMax = 3;
	public float wallStickTime = .25f;
	float timeToWallUnstick;
	float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	public int maxHealth = 5;
	[HideInInspector]
	public bool dead = false;

	[HideInInspector]
	public int health;

	//[HideInInspector]
	public CheckPoint current_checkpoint;
	
	[HideInInspector]
	public bool runHeld;
	[HideInInspector]
	public bool movementDisabled;
	[HideInInspector]
	public bool freezePosition;
	[HideInInspector]
	public Vector3 velocity;
	[HideInInspector]
	int wallDirX;
	[HideInInspector]
	float velocityXSmoothing;
	[HideInInspector]
	public bool wallSliding;
	[HideInInspector]
	public bool attackInputDown;
	[HideInInspector]
	public bool attackInputUp;
	[HideInInspector]
	public bool runInputDown;
	[HideInInspector]
	public bool runInputUp;
	[HideInInspector]
	public PlayerState state;
	[HideInInspector]
	public Vector2 input_prev;
	[HideInInspector]
	public Controller2D controller;
	[HideInInspector]
	public Vector2 directionalInput;
	[HideInInspector]
	public bool invincible;
	[HideInInspector]
	public Vector2 wallJumpClimb;
	[HideInInspector]
	public Vector2 wallJumpOff;
	[HideInInspector]
	public Vector2 wallLeap;
	[HideInInspector]
	public Vector2 addedVelocity;
	void Start() {
		controller = GetComponent<Controller2D> ();

		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);

		invincible = false;
		dead = false;
		movementDisabled = false;
		freezePosition = false;

		health = maxHealth;

		GetComponent<Entry>().enabled = true;

		// AudioManager am = AudioManager.instance;
        // am.play("theme");
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

		if(!freezePosition)
		{
			controller.Move (velocity * Time.deltaTime, directionalInput);
		}
		
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
		if(!movementDisabled)
		{
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
	}

	public void OnJumpInputUp() {
		if(!movementDisabled) {
			if (velocity.y > minJumpVelocity) {
				velocity.y = minJumpVelocity;
			}
		}
	}

	public void OnAttackInputDown() {
		attackInputDown = true;
	}

	public void OnAttackInputUp() {
		attackInputUp = true;
	}

	public void OnRunInputDown() {
		runInputDown = true;
	}

	public void OnRunInputUp() {
		runInputUp = true;
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
		float targetVelocityX;
		if(!movementDisabled)
		{
			targetVelocityX = directionalInput.x * moveSpeed;
		} else {
			targetVelocityX = 0.0f;
		}
		
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;

		velocity.x += addedVelocity.x;
		velocity.y += addedVelocity.y;

		addedVelocity.x = 0;
		addedVelocity.y = 0;
	}

	public void TakeDamage(int damage, float invulnDuration)
	{
		IEnumerator damageflash = DamageFlash(Color.white, invulnDuration, 0.05f);
		invincible = true;
		StartCoroutine(damageflash);
		health -= damage;
		if(health <= 0)
		{
			state.EnterDeath();
		}
	}

	public void Respawn()
	{
		dead = false;

		transform.position = current_checkpoint.transform.position;
        transform.rotation = current_checkpoint.transform.rotation;

        state.EnterIdle();

        movementDisabled = false;
		freezePosition = false;
		velocity = new Vector3(0, 0, 0);

        health = maxHealth;

        if(current_checkpoint.facing_direction < 0)
        {
          state.animator.Play("IdleL", -1, 0f);
        } else {
          state.animator.Play("IdleR", -1, 0f);
        }

		AudioManager am = AudioManager.instance;
        am.stop("death");
        am.play("theme");
	}

	public void freeze(float seconds)
	{
		StartCoroutine(FreezePlayer(seconds));
	}

	private IEnumerator FreezePlayer(float seconds)
    {
		state.animator.speed = 0;
		freezePosition = true;
		this.state.enabled = false;
		yield return new WaitForSeconds(seconds);
		this.state.enabled = true;
		state.animator.speed = 1;
		freezePosition = false;
	}

     public IEnumerator DamageFlash(Color flashColor, float flashTime, float flashSpeed)
     {
		SkinnedMeshRenderer renderer = transform.Find("Character").GetComponent<SkinnedMeshRenderer>();
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