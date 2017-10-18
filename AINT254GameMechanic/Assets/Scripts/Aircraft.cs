using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft : MonoBehaviour {

    
    private Rigidbody rb;

    private float currentSpeed;
    private float minAltitudeSpeed = 50.0f;
    private float maxSpeed = 150.0f;
    private float breakSpeed = 500.0f;
    private float torque = 10.0f;

    private bool flyingSpeed = false;
    private float inputValue;
    private bool inputSpace;
    private bool inputC;


    // acceleration and decelleration
    private float speed = 0.0f;
    private float timeZeroToMax = 15f;
    private float accRatePerSec;
    

    // Use this for initialization
    void Start () {
        accRatePerSec = maxSpeed / timeZeroToMax;

        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        currentSpeed = rb.velocity.magnitude;
        Debug.Log(rb.velocity.magnitude);

        if (currentSpeed >= minAltitudeSpeed)
        {
            flyingSpeed = true;
        }
        else
        {
            flyingSpeed = false;
        }

        // if min altitude speed achieved
        if (flyingSpeed)
        {
            // allow player to control torque
            rb.AddTorque(transform.right * torque * -inputValue);

            Debug.Log("fly");
        }

        // acceleration
        if (inputSpace)
        {
            speed += accRatePerSec * Time.deltaTime;
        }
        
        // decelleration
        if (inputC)
        {
            speed -= accRatePerSec * Time.deltaTime;
        }

        // if current speed is less than max speed
        if (currentSpeed <= maxSpeed)
        {
            // add velocity
            rb.velocity = transform.forward * speed;
            Debug.Log("acceleration");

        }
        else
        {
            // keep current speed at max speed
            currentSpeed = maxSpeed;
        }




    }

    private void Update()
    {
        inputValue = Input.GetAxis("Vertical");
        inputSpace = Input.GetKey(KeyCode.Space);
        inputC = Input.GetKey(KeyCode.C);
    }
}
