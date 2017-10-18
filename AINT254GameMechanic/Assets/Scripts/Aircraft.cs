using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft : MonoBehaviour {

    
    private Rigidbody rb;

    private float currentSpeed;
    private float minAltitudeSpeed = 100.0f;
    private float maxSpeed = 150.0f;

    [SerializeField]
    private float torque = 5.0f;
    [SerializeField]
    private float torqueTurn = 5.0f;

    private bool flyingSpeed = false;
    private float inputValue;
    private bool inputSpace;
    private bool inputC;
    private float inputValueTurn;

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
            torque = currentSpeed / 2.5f;

            torqueTurn = currentSpeed / 2.5f;
            // allow player to control torque
            rb.AddRelativeTorque(torque * -inputValue, 0, torqueTurn * -inputValueTurn);

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
        




    }

    private void Update()
    {
        inputValue = Input.GetAxis("Vertical");
        inputSpace = Input.GetKey(KeyCode.Space);
        inputC = Input.GetKey(KeyCode.C);
        inputValueTurn = Input.GetAxis("Horizontal");
    }
}
