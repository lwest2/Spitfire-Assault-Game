using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerProjectile : MonoBehaviour {

    private float force = 10.0f;
    private Rigidbody m_rb;
    private Transform m_transform;

    // Use this for initialization
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_transform = GetComponent<Transform>();

        Destroy(gameObject, 10.0f);
    }

    void FixedUpdate()
    {
        m_rb.AddForce(m_transform.forward * force, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Map")
        {
            Destroy(gameObject);
        }
    }
}
