using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public bool dead = false;

    public float health = 10;
    private float cldn = 0;
    public float atkCooldown = 2f;
    public float Damage = 2;
    public GameObject projectile;
    public GameObject projectileSpawn;

    [SerializeField]
    private Rigidbody body;
    private System.Random rander;
    private FOVCone fov;

    public enum type
    {
        chomper, spitter, drone, elite, boss
    }

    public type enemy = type.chomper;

    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody>();
        rander = new System.Random();
        fov = GetComponent<FOVCone>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            dead = true;
        }

        if (!dead)
        {
            switch (enemy)
            {
                case type.chomper:
                    ChomperUpdate();
                    break;

                case type.spitter:
                    SpitterUpdate();
                    break;

                case type.drone:
                    DroneUpdate();
                    break;

                case type.elite:
                    EliteUpdate();
                    break;

                case type.boss:
                    BossUpdate();
                    break;
            }
        }
    }

    void ChomperUpdate()
    {
        if (cldn <= 0)
        {
            // time to attack
        }
        else
        {
            cldn -= Time.deltaTime;
        }
    }

    void SpitterUpdate()
    {
      
        if (cldn <= 0)
        {
            // chance to shoot
            float rand = Random.Range(0.0f, 60.0f);
            if (rand < 3)
            {
                Instantiate(projectile, projectileSpawn.transform.position, Quaternion.identity);
                cldn = atkCooldown;
            }

        }
        else
        {
            cldn -= Time.deltaTime;
        }
    }

    void DroneUpdate()
    {

        if (cldn <= 0)
        {
            // chance to shoot
            float rand = Random.Range(0.0f, 60.0f);
            if (rand < 1)
            {
                Instantiate(projectile, projectileSpawn.transform.position, Quaternion.identity);
                cldn = atkCooldown;
            }
        }
        else
        {
            cldn -= Time.deltaTime;
        }
    }

    void EliteUpdate()
    {

        if (cldn <= 0)
        {
            // chance to shoot
            float rand = Random.Range(0.0f, 60.0f);
            if (rand < 1)
            {
                Instantiate(projectile, projectileSpawn.transform.position, Quaternion.identity);
                cldn = atkCooldown;
            }
        }
        else
        {
            cldn -= Time.deltaTime;
        }
    }

    void BossUpdate()
    {

        if (cldn <= 0)
        {
            // chance to shoot
            float rand = Random.Range(0.0f, 60.0f);
            if (rand < 1)
            {
                Instantiate(projectile, projectileSpawn.transform.position, Quaternion.identity);
                cldn = atkCooldown;
            }
        }
        else
        {
            cldn -= Time.deltaTime;
        }
    }
}
