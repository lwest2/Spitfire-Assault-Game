using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerProjectile : MonoBehaviour {

    private float force = 10.0f;
    private Rigidbody m_rb;
    private Transform m_transform;

    void OnEnable()
    {
        Invoke("Die", 4.0f);  
    }

    // Use this for initialization
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_transform = GetComponent<Transform>();
       
    }

    void FixedUpdate()
    {
        m_rb.AddForce(m_transform.forward * force, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Map")
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void OnDisable()
    {
        CancelInvoke("Die");
    }
}
