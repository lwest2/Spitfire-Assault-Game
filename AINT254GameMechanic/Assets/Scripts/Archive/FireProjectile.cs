using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour {

    // force for enemy projectile
    private float m_force = 50.0f;
    private Rigidbody m_rb;
    playerGUIhelper playergui;

	// Use this for initialization
	void Start () {
        m_rb = GetComponent<Rigidbody>();
        
        // destroy enemy projectile within 7 seconds
        Destroy(gameObject, 7.0f);
    }

    void FixedUpdate()
    {
        // add constant velocity
        m_rb.velocity = transform.forward * m_force;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if collides with player
        if (collision.collider.tag == "Player")
        {
            // destroy projectile
            Destroy(gameObject);
            // deduct players health
            playerGUIhelper.playergui.setPlayerHealth(-10.0f);
        }
    }
    
}
