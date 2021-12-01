using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneProjectile : MonoBehaviour
{
    public int damage = 1;
    public float speed = 4.0f;
    private GameObject player;
    private float lifespan = 1.0f;

    public GameObject projectile;
    public GameObject projectileSpawn;

    

    void Start()
    {
        clone = Instantiate(projectile, projectileSpawn);
        projectile.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    void Update()
    {

        // If timer runs out for projectile, it will disappear
        if (lifespan <= 0)
        {
            Destroy(projectile);
        }
        else
        {
            lifespan -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter3D(Collider collision)
    {
        // If it hits anything, the projectile will disappear
        if (collision.gameObject.tag.Equals("Obstacles") || collision.gameObject.tag.Equals("hurtbox") || collision.gameObject.tag.Equals("hitbox"))
        {
            Destroy(projectile);
        }
    }
}
