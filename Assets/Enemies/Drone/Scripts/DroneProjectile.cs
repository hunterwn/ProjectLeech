using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneProjectile : MonoBehaviour
{
    public int damage = 1;
    public float speed = 4.0f;
    private GameObject player;
    private float lifespam = 1.0f;
    public float cldn = 0.0f;
    public float shotCldn = 1.0f;

    public GameObject projectile;
    public GameObject projectileSpawn;

    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {

        if (cldn <= 0)
        {
            // chance to shoot
            float rand = Random.Range(0.0f, 100.0f);
            if (rand < 1)
            {
                gameObject.GetComponent<Animator>().SetTrigger("shoot");
                GameObject bullet = Instantiate(projectile, projectileSpawn.transform.position, Quaternion.identity);

                cldn = shotCldn;
            }
        }
        else
        {
            cldn -= Time.deltaTime;
        }

        // If timer runs out for projectile, it will disappear
        if (lifespam <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            lifespam -= Time.deltaTime;
        }
        
        // This makes it so it homes in on the target, gotta change this part lul
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter3D(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            PlayerDamageController controller = collision.gameObject.GetComponent<PlayerDamageController>();
            Vector3 vel = new Vector3();

            collision.gameObject.GetComponent<Animator>().SetTrigger("hit");
            collision.gameObject.GetComponent<Rigidbody>().velocity = vel;
        }

        // If it hits anything, the projectile will disappear
        if (!collision.gameObject.tag.Equals("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
