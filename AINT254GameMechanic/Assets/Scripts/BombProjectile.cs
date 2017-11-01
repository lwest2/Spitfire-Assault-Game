using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : MonoBehaviour {

    // reference to script for damaging turrets
    private TurretTakeDamage m_turretBehaviour;

    // Use this for initialization
    void Start () {
        // destroy the bomb projectile within 10 seconds if not done so already
        Destroy(gameObject, 10.0f);
    }
	

    void OnCollisionEnter(Collision other)
    {
        // if the bomb collides with the map
        if (other.collider.tag == "Map")
        {
            // destroy the bomb object
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // if the trigger collides with the enemy
        if (other.tag == "Enemy")
        {
            // get reference to the turret hit
            m_turretBehaviour = other.gameObject.GetComponent<TurretTakeDamage>();
            // set the health of the turret to -20
            m_turretBehaviour.setEnemyHealth(-20.0f);
            // destroy the bomb object
            Destroy(gameObject);
        }
    }
}
