using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class PlayerController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Entry>().enabled = true;
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}