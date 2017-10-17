using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft : MonoBehaviour {

    
    private Rigidbody rb;

    private float currentSpeed;
    private float applySpeed = 1000.0f;
    private float minAltitudeSpeed = 150.0f;
    private float maxSpeed = 250.0f;
    private float breakSpeed = 500.0f;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        currentSpeed = rb.velocity.magnitude;

        

        if (Input.GetKey(KeyCode.W))
        {
            if (currentSpeed >= maxSpeed)
            {
                currentSpeed = maxSpeed;
                Debug.Log("Staying at max speed");
            }
            else
            {
                rb.AddRelativeForce(Vector3.forward * applySpeed * Time.deltaTime * 0.5f, ForceMode.Acceleration);
                Debug.Log(rb.velocity.magnitude);
            }
            if (currentSpeed >= minAltitudeSpeed)
            {
                
                Debug.Log("Fly");
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddRelativeForce(Vector3.back * breakSpeed * Time.deltaTime, ForceMode.Acceleration);
        }
        
	}
}
