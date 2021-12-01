using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float bulletSpeed = 1.5f;

    Vector3 shootDir;

    // Start is called before the first frame update
    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
        Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += shootDir * bulletSpeed * Time.deltaTime;
    }
}