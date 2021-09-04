using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class PlayerController : MonoBehaviour
    {
        public float gravity;
        public float ground_friction;
        public float air_friction;
        public float run_acceleration;
        public float walk_acceleration;
        public float aerial_drift;
        public float run_maxspeed;
        public float walk_maxspeed;
        public float max_airdriftspeed;
        public float current_speed_h;
        public float current_speed_v;
        public float jump_initial_velocity;

        public CharacterController controller;

        void Start()
        {
            controller = GetComponent<CharacterController>();
            GetComponent<Entry>().enabled = true;
        }
        void Update()
        {
            if(controller.isGrounded && Mathf.Abs(current_speed_v) < 0.01f)
            {
                current_speed_v = -0.01f;
            }
            Vector3 move = new Vector3(current_speed_h, current_speed_v, 0);
            controller.Move(move);
        }
    }
}