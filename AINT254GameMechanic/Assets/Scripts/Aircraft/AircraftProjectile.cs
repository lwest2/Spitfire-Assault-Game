using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AircraftProjectile : MonoBehaviour {

    // player bullet speed
    private float m_force = 90.0f;
    private Rigidbody m_rb;


    void OnEnable()
    {
        // invoke death
        Invoke("Die", 2f);
    }

    // Use this for initialization
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // if active have forward momentum
        if(gameObject.activeInHierarchy)
        m_rb.velocity = transform.forward * m_force;
    }

    void Die()
    {
        // once dead
        if (gameObject.activeInHierarchy)
        {
            // disable
            gameObject.SetActive(false);
            // reset forward momentum
            m_rb.velocity = transform.forward * 0;
        }
    }

    void OnDisable()
    {
        // cancel invoke on disable
        CancelInvoke("Die");
    }
}
