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
        Invoke("Die", 1.0f);
    }

    // Use this for initialization
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        m_rb.velocity = transform.forward * m_force;
    }

    void Die()
    {
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        // cancel invoke on disable
        CancelInvoke("Die");
    }
}
