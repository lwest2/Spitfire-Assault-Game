using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft : MonoBehaviour {

    // references: https://keithmaggio.wordpress.com/2011/07/01/unity-3d-code-snippet-flight-script/
    // http://answers.unity3d.com/questions/554291/finding-a-rigidbodys-rotation-speed-and-direction.html
    // http://answers.unity3d.com/questions/10425/how-to-stabilize-angular-motion-alignment-of-hover.html

    private Rigidbody m_rb;             // rigidbody for aircraft
    private Animator m_anim;

    // checks (speed)
    private float m_currentSpeed;       // current speed for speed checks
    
    // input
    private float m_inputYaw;           // input for yaw
    private float m_inputPitch;         // input for pitch
    private float m_inputRoll;          // input for roll
    private bool m_inputDeceleration;   // input for deceleration
    private bool m_inputAcceleration;   // input for acceleration

    // acceleration and decelleration
    private float m_speed = 20.0f;          // starting speed
    private float m_timeZeroToMax = 15f;    // how long it will take to reach max speed with acceleration
    private float m_accRatePerSec;          // the acceleration
    private float m_maxSpeed = 30.0f;       // max velocity
    private float m_rotationSpeed = 30.0f;

    //yaw, pitch, roll
    private Quaternion m_AddRot = Quaternion.identity;  // rotation to add
    private float m_roll = 0;   // roll value
    private float m_pitch = 0;  // pitch value  
    private float m_yaw = 0;    // yaw value
    private float m_roll2 = 0;  // additional roll

    private Vector3 m_predictUp;    // prediction of the up vector
    private Vector3 m_torqueVector; // how much torque should be added

 

    // Use this for initialization
    void Start () {
        

        m_accRatePerSec = m_maxSpeed / m_timeZeroToMax; // gets acceleration velocitys

        m_rb = GetComponent<Rigidbody>();
        m_anim = GetComponent<Animator>();

        
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        // current speed
        m_currentSpeed = m_rb.velocity.magnitude;

        

        
            m_pitch = m_inputPitch * (Time.deltaTime * m_rotationSpeed);    // pitch using input
            m_yaw = m_inputYaw * (Time.deltaTime * m_rotationSpeed);        // yaw using input
            m_roll = m_inputYaw * (Time.deltaTime * m_rotationSpeed);       // roll while with yaw input

            m_roll2 = m_inputRoll * (Time.deltaTime * m_rotationSpeed);     // additional roll input
        
        // if current speed is less than max speed
        if (m_currentSpeed < m_maxSpeed)
        {
            // if there is input for acceleration
            if (m_inputAcceleration)
            { 
            Debug.Log("Acceleration");
                // accelerate
                m_speed += m_accRatePerSec * Time.deltaTime;
            }
        }

        // if current speed is above 10
        if (m_currentSpeed > 19)
        {
            // if there is input for deceleration
            if (m_inputDeceleration)
            {
                Debug.Log("Deceleration");
                // decelerate
                m_speed -= m_accRatePerSec * Time.deltaTime;
            }
        }

        // if there is no roll input
        if (m_inputRoll == 0)
        {
            // predict the up transform
            m_predictUp = Quaternion.AngleAxis(m_rb.angularVelocity.magnitude * Mathf.Rad2Deg * 0.6f / 3.0f, m_rb.angularVelocity) * transform.up;
            // get the cross vector between the predicted up transformation and the current up transformation
            m_torqueVector = Vector3.Cross(m_predictUp, Vector3.up);
            // add torque
            m_rb.AddTorque(m_torqueVector * 1.0f * 1.0f);
        }

        // add velocity (there might be new velocity from acceleration and deceleration)
        m_rb.velocity = transform.forward * m_speed;

        // create euler angles from the inputs
        m_AddRot.eulerAngles = new Vector3(-m_pitch, -m_yaw, m_roll + m_roll2);

        // add rotation to rigidbody
        m_rb.rotation *= m_AddRot;

        if (m_inputPitch > 0)
        {
            m_anim.SetBool("Elevate", true);

        }
        else
        {
            m_anim.SetBool("Elevate", false);
        }


        if (m_inputPitch < 0)
        {
            m_anim.SetBool("Deelevate", true);
        }
        else
        {
            m_anim.SetBool("Deelevate", false);
        }

        
        if (m_inputYaw > 0)
        {
            m_anim.SetBool("Roll_left", true);
        }
        else
        {
            m_anim.SetBool("Roll_left", false);
        }

        if(m_inputYaw < 0)
        {
            m_anim.SetBool("Roll_right", true);
        }
        else
        {
            m_anim.SetBool("Roll_right", false);
        }
    }

    private void Update()
    {       
        // inputs for xbox controls
        m_inputYaw = Input.GetAxis("yaw");
        m_inputPitch = Input.GetAxis("pitch");
        m_inputRoll = Input.GetAxis("roll");
        m_inputDeceleration = Input.GetKey("joystick button 4");
        m_inputAcceleration = Input.GetKey("joystick button 5");


    }


}
