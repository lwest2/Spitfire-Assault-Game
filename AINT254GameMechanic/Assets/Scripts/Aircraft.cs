using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft : MonoBehaviour {

    
    private Rigidbody rb;

    private float currentSpeed;
    private float applySpeed = 1000.0f;
    private float minAltitudeSpeed = 50.0f;
    private float maxSpeed = 250.0f;
    private float breakSpeed = 500.0f;

    [SerializeField]
    private float torque = 50.0f;

    private bool flyingSpeed = true;
    private float inputValue;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (flyingSpeed)
        {
            rb.AddTorque(transform.right * torque * inputValue);
            rb.velocity = transform.forward * 5;
        }
        //currentSpeed = rb.velocity.magnitude;

        //if (Input.GetKey(KeyCode.W))
        //{
        //    if (currentSpeed >= maxSpeed)
        //    {
        //        currentSpeed = maxSpeed;
        //        Debug.Log("Staying at max speed");
        //    }
        //    else
        //    {
        //        rb.AddRelativeForce(transform.forward * applySpeed * Time.deltaTime * 0.5f, ForceMode.Acceleration);
        //        Debug.Log(rb.velocity.magnitude);
        //    }

        //    if (currentSpeed >= minAltitudeSpeed)
        //    {                
        //        rb.AddRelativeTorque(transform.right * torque);
        //        Debug.Log("Fly");
        //    }
        //}

        //if (Input.GetKey(KeyCode.S))
        //{
        //    rb.AddRelativeForce(-transform.forward * breakSpeed * Time.deltaTime, ForceMode.Acceleration);
        //}

    }

    private void Update()
    {
        inputValue = Input.GetAxis("Vertical");
    }
}
