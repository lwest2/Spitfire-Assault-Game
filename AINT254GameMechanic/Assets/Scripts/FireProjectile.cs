using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour {

    private float force = 7.0f;
    private Rigidbody m_rb;
    private Transform m_transform;

	// Use this for initialization
	void Start () {
        m_rb = GetComponent<Rigidbody>();
        m_transform = GetComponent<Transform>();
       
        Destroy(gameObject, 5.0f);
        
	}

    void FixedUpdate()
    {       
        m_rb.AddForce(m_transform.forward * force, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            Destroy(gameObject);
            Debug.Log("HIT Deduct health here: FireProjectile");
        }
    }
}
