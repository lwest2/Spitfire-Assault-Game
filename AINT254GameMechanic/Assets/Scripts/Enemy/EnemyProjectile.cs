using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour {

    // player bullet speed
    private float m_force = 120.0f;
    private Rigidbody m_rb;

    void OnEnable()
    {
        // invoke death
        Invoke("Die", 10f);
    }

    // Use this for initialization
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (gameObject.activeInHierarchy)
            m_rb.velocity = transform.forward * m_force;
    }

    void Die()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
            m_rb.velocity = transform.forward * 0;
        }
    }

    void OnDisable()
    {
        // cancel invoke on disable
        CancelInvoke("Die");
    }
}
