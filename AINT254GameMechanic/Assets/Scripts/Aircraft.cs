using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft : MonoBehaviour {

    // references: https://keithmaggio.wordpress.com/2011/07/01/unity-3d-code-snippet-flight-script/
    // http://answers.unity3d.com/questions/554291/finding-a-rigidbodys-rotation-speed-and-direction.html

    private Rigidbody rb;

    // checks (speed)
    private float currentSpeed;
    
    // input
    private float inputYaw;
    private float inputPitch;
    private float inputRoll;
    private float inputDeceleration;
    private float inputAcceleration;

    // acceleration and decelleration
    private float speed = 7.0f;
    private float timeZeroToMax = 15f;
    private float accRatePerSec;
    private float maxSpeed = 30.0f;
    private float rotationSpeed = 30.0f;

    //yaw, pitch, roll
    private Quaternion AddRot = Quaternion.identity;
    private float roll = 0;
    private float pitch = 0;
    private float yaw = 0;
    
    // Use this for initialization
    void Start () {
        accRatePerSec = maxSpeed / timeZeroToMax;

        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        currentSpeed = rb.velocity.magnitude;
        //Debug.Log(rb.velocity.magnitude);

        

        if (SwitchCamera.m_cameraSwitcher)
        {
            pitch = inputPitch * (Time.deltaTime * rotationSpeed);
            yaw = inputYaw * (Time.deltaTime * rotationSpeed);
            roll = inputRoll * (Time.deltaTime * rotationSpeed);
        }
    

        if (currentSpeed < maxSpeed)
        {
            //Debug.Log("Acceleration");
            speed += inputAcceleration * (accRatePerSec * Time.deltaTime);
        }

        if (currentSpeed > 5)
        {
            //Debug.Log("Deceleration");
            speed -= inputDeceleration * (accRatePerSec * Time.deltaTime);
        }
        

        
        rb.velocity = transform.forward * speed;

        AddRot.eulerAngles = new Vector3(pitch, yaw, -roll);
        rb.rotation *= AddRot;
        

    }

    private void Update()
    {       
        inputYaw = Input.GetAxis("yaw");
        inputPitch = Input.GetAxis("pitch");
        inputRoll = Input.GetAxis("roll");
        inputDeceleration = Input.GetAxis("left trigger");
        inputAcceleration = Input.GetAxis("right trigger");
    }


}
