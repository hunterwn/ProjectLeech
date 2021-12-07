using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour {
    private SphereCollider coll;
    public float size;
    private void OnEnable() {
        if(this.coll == null)
        {
            this.coll = gameObject.AddComponent<SphereCollider>();
            this.coll.isTrigger = true;
            this.coll.tag = "hitbox";
        }
        this.coll.enabled = true;
        this.coll.radius = size / 1000;
    }
    private void OnDisable() {
        this.coll.enabled = false;
        this.coll.radius = 0.0f;
    }

    private void OnTriggerEnter(Collider collider) {
        if(collider.tag == "Enemy")
        {
            Enemy enemy = collider.transform.parent.gameObject.GetComponent<Enemy>();
            enemy.Stun(1.0f);
        }
    }
}