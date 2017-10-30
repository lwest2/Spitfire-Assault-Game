using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft : MonoBehaviour {

    // references: https://keithmaggio.wordpress.com/2011/07/01/unity-3d-code-snippet-flight-script/
    // http://answers.unity3d.com/questions/554291/finding-a-rigidbodys-rotation-speed-and-direction.html
    // http://answers.unity3d.com/questions/10425/how-to-stabilize-angular-motion-alignment-of-hover.html

    private Rigidbody m_rb;

    // checks (speed)
    private float m_currentSpeed;
    
    // input
    private float m_inputYaw;
    private float m_inputPitch;
    private float m_inputRoll;
    private bool m_inputDeceleration;
    private bool m_inputAcceleration;

    // acceleration and decelleration
    private float m_speed = 7.0f;
    private float m_timeZeroToMax = 15f;
    private float m_accRatePerSec;
    private float m_maxSpeed = 30.0f;
    private float m_rotationSpeed = 30.0f;

    //yaw, pitch, roll
    private Quaternion m_AddRot = Quaternion.identity;
    private float m_roll = 0;
    private float m_pitch = 0;
    private float m_yaw = 0;
    private float m_roll2 = 0;

    private Vector3 m_predictUp;
    private Vector3 m_torqueVector;

    // Use this for initialization
    void Start () {
        m_accRatePerSec = m_maxSpeed / m_timeZeroToMax;

        m_rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        m_currentSpeed = m_rb.velocity.magnitude;
        //Debug.Log(m_rb.velocity.magnitude);

        

        if (SwitchCamera.m_cameraSwitcher)
        {
            m_pitch = m_inputPitch * (Time.deltaTime * m_rotationSpeed);
            m_yaw = m_inputYaw * (Time.deltaTime * m_rotationSpeed);
            m_roll = m_inputYaw * (Time.deltaTime * m_rotationSpeed);

            m_roll2 = m_inputRoll * (Time.deltaTime * m_rotationSpeed);
        }

        if (m_currentSpeed < m_maxSpeed)
        {
            if (m_inputAcceleration)
            { 
            Debug.Log("Acceleration");
            m_speed += m_accRatePerSec * Time.deltaTime;
            }
        }

        if (m_currentSpeed > 5)
        {
            if (m_inputDeceleration)
            {
                Debug.Log("Deceleration");
                m_speed -= m_accRatePerSec * Time.deltaTime;
            }
        }

        if (m_inputRoll == 0)
        {
            // predict the up transformation
            m_predictUp = Quaternion.AngleAxis(m_rb.angularVelocity.magnitude * Mathf.Rad2Deg * 0.6f / 1.0f, m_rb.angularVelocity) * transform.up;

            // get the cross vector between the predicted up transformation and the current up transformation
            m_torqueVector = Vector3.Cross(m_predictUp, Vector3.up);
            // add torque
            m_rb.AddTorque(m_torqueVector * 1.0f * 1.0f);
        }

        m_rb.velocity = transform.forward * m_speed;

        m_AddRot.eulerAngles = new Vector3(m_pitch, m_yaw, -m_roll + -m_roll2);

        m_rb.rotation *= m_AddRot;


    }

    private void Update()
    {       
        m_inputYaw = Input.GetAxis("yaw");
        m_inputPitch = Input.GetAxis("pitch");
        m_inputRoll = Input.GetAxis("roll");
        m_inputDeceleration = Input.GetKey("joystick button 4");
        m_inputAcceleration = Input.GetKey("joystick button 5");
    }


}
