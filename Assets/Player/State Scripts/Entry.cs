using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class Entry : MonoBehaviour
    {
        void OnEnable() {
            this.enabled = false;
            GetComponent<Idle>().enabled = true;
        }
    }
}