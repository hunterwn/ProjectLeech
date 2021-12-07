using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float bulletSpeed = 2.0f;

    Vector3 shootDir;
    Transform bulletHitEffect;

    // Start is called before the first frame update
    public void Setup(Vector3 shootDir, Transform bulletHitEffect)
    {
        this.shootDir = shootDir;
        this.bulletHitEffect = bulletHitEffect;
        Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += shootDir * bulletSpeed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.collider.tag == "Player" || collision.gameObject.layer == 8)
        {
            Transform bulletHitEffectTransform = Instantiate(this.bulletHitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}