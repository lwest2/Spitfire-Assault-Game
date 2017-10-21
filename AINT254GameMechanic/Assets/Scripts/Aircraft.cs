using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft : MonoBehaviour {

    // references: https://keithmaggio.wordpress.com/2011/07/01/unity-3d-code-snippet-flight-script/
    // http://answers.unity3d.com/questions/554291/finding-a-rigidbodys-rotation-speed-and-direction.html

    private Rigidbody rb;

    // checks (speed)
    private float currentSpeed;
    private bool flyingSpeed = false;
    private float minAltitudeSpeed = 100.0f;
    
    
    // input
    private float inputValue;
    private bool inputSpace;
    private bool inputC;
    private float inputValueTurn;
    private float inputYaw;

    // acceleration and decelleration
    private float speed = 0.0f;
    private float timeZeroToMax = 15f;
    private float accRatePerSec;
    private float maxSpeed = 150.0f;
    private float rotationSpeed = 50.0f;

    //yaw, pitch, roll
    private Quaternion AddRot = Quaternion.identity;
    private float roll = 0;
    private float pitch = 0;
    private float yaw = 0;
    private Quaternion lastRot = Quaternion.identity;

    // Use this for initialization
    void Start () {
        accRatePerSec = maxSpeed / timeZeroToMax;

        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        lastRot = transform.rotation;
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
           
            roll = inputValueTurn * (Time.deltaTime * rotationSpeed);
            pitch = inputValue * (Time.deltaTime * rotationSpeed);
            yaw = inputYaw * (Time.deltaTime * rotationSpeed);

            Debug.Log("fly");
        }

        else
        {
            yaw = inputYaw * (Time.deltaTime * rotationSpeed);
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

        AddRot.eulerAngles = new Vector3(-pitch, yaw, -roll);
        rb.rotation *= AddRot;



    }

    private void Update()
    {
        inputValue = Input.GetAxis("Vertical");
        inputSpace = Input.GetKey(KeyCode.Space);
        inputC = Input.GetKey(KeyCode.C);
        inputValueTurn = Input.GetAxis("Horizontal");
        inputYaw = Input.GetAxis("yaw");
    }

    private void OnTriggerEnter(Collider other)
    {
        flyingSpeed = false;
        rb.rotation = transform.rotation = Quaternion.Euler(new Vector3(lastRot.x, 0, lastRot.z));
        Debug.Log("Landing");
        if (rb.velocity.magnitude > 90)
        {
            speed = 89;
            Debug.Log("Slowing: " + speed);
        }
        

    }
}
