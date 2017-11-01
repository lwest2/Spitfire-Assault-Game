using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerProjectile : MonoBehaviour {

    // set force of player bullets to 60
    private float m_force = 60.0f;
    private Rigidbody m_rb;
    
    private TurretTakeDamage m_turretBehaviour;

    void OnEnable()
    {
        // invoke death in 4 seconds
        Invoke("Die", 4.0f);  
    }

    // Use this for initialization
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        
    }

    void FixedUpdate()
    {
        // constant velocity
        m_rb.velocity = transform.forward * m_force;
    }

    void OnCollisionEnter(Collision other)
    {
        // if collision with map
        if (other.collider.tag == "Map")
        {
            Die();
        }
        // if collision with enemy projectile
        if (other.collider.tag == "Projectile")
        {
            Die();
            Destroy(other.gameObject);
        }
        // if collision with enemy
        if (other.collider.tag == "Enemy")
        {
            Die();
        }

        // if collision with balloon
        if (other.collider.tag == "Balloon")
        {
            // destroy balloon
            Destroy(other.gameObject);
        }

        // if collision with crate
        if (other.collider.tag == "Crate")
        {
            // give bomb
            playerGUIhelper.playergui.setPlayerBombs(1.0f);
            // destroy crate
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //f collides with enemy trigger
        if (other.tag == "Enemy")
        {
            // destroy bullet
            Die();
            // deduct enemy health
            m_turretBehaviour = other.gameObject.GetComponent<TurretTakeDamage>();
            m_turretBehaviour.setEnemyHealth(-1.0f);
        }
        
    }
    void Die()
    {
        // destroy bullet
        Destroy(gameObject);
    }

    void OnDisable()
    {
        // cancel invoke on disable
        CancelInvoke("Die");
        // will implement pool manager
    }
}
