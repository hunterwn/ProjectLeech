using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
  public class PlayerController : MonoBehaviour {
    public float gravity;
    public float ground_friction;
    public float air_friction;
    public float run_acceleration;
    public float walk_acceleration;
    public float aerial_drift;
    public float run_maxspeed;
    public float walk_maxspeed;
    public float max_airdriftspeed;

    public float jump_initial_velocity;
    public float jump_aerial_initial_velocity;
    public int jumpSquatFrames;
    public int max_jumps;
    [HideInInspector] public float current_speed_h;
    [HideInInspector] public float current_speed_v;
    [HideInInspector] public int jumps_left;
    [HideInInspector] public bool airstate;
    public CharacterController controller;

    [SerializeField]
    PlayerState[] playerStates;

    void Start() {
      //initialize components
      controller = GetComponent<CharacterController>();

      //initialize attributes
      jumps_left = max_jumps;
      current_speed_h = 0;
      current_speed_v = 0;
      airstate = false;

      //Enter initial state
      GetComponent<Entry>().enabled = true;
    }
    void Update() {

      //Keep player attached to the ground
      if (controller.isGrounded && Mathf.Abs(current_speed_v) < 0.01f) {
        current_speed_v = -0.01f;
      }

      // Move player based on current speed values
      Vector3 move = transform.forward * current_speed_h + transform.up * current_speed_v;
      controller.Move(move);
    }
  }
}