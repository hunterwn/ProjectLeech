using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class PlayerController : MonoBehaviour
    {
        public float ground_friction;
        public float run_acceleration;
        public float walk_acceleration;
        public float run_maxspeed;
        public float walk_maxspeed;
        public float current_speed;

        void Start()
        {
            GetComponent<Entry>().enabled = true;
        }
        void Update()
        {
            transform.Translate(Vector3.right * current_speed, Space.World);
        }
    }
}