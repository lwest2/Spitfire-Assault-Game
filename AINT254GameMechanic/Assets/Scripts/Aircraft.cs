using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft : MonoBehaviour {

    // references: https://keithmaggio.wordpress.com/2011/07/01/unity-3d-code-snippet-flight-script/
    // http://answers.unity3d.com/questions/554291/finding-a-rigidbodys-rotation-speed-and-direction.html
    // http://answers.unity3d.com/questions/10425/how-to-stabilize-angular-motion-alignment-of-hover.html
    // https://answers.unity.com/questions/1052724/how-to-have-a-speed-boost-in-my-game.html

    private Rigidbody m_rb;             // rigidbody for aircraft
    private Animator m_anim;
    
    // input
    private float m_inputYaw;           // input for yaw
    private float m_inputPitch;         // input for pitch
    private float m_inputRoll;          // input for roll
    private bool m_inputAcceleration;   // input for acceleration

    // acceleration and decelleration
    private float m_speed = 25.0f;          // starting speed
    private float m_maxSpeed = 28.0f;       // max velocity
    private float m_rotationSpeed = 30.0f;
    private float m_acceleration = 0.025f;
    private float m_initialSpeed;

    // boost
    private float m_speedBoost = 1f;
    private float m_speedBoostMax = 40f;
    private bool hasComplete = false;
    private bool hasCompleteDe = false;

    //yaw, pitch, roll
    private Quaternion m_AddRot = Quaternion.identity;  // rotation to add
    private float m_roll = 0;   // roll value
    private float m_pitch = 0;  // pitch value  
    private float m_yaw = 0;    // yaw value
    private float m_roll2 = 0;  // additional roll

    private Vector3 m_predictUp;    // prediction of the up vector
    private Vector3 m_torqueVector; // how much torque should be added   

    public bool getComplete()
    {
        return hasComplete;
    }

    public bool getCompleteDe()
    {
        return hasCompleteDe;
    }

    // Use this for initialization
    void Start () {       
        m_rb = GetComponent<Rigidbody>();
        m_anim = GetComponent<Animator>();

        m_initialSpeed = m_speed;
        
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Fly();
        Animate();
    }

    private void Update()
    {       
        // inputs for xbox controls
        m_inputYaw = Input.GetAxis("yaw");
        m_inputPitch = Input.GetAxis("pitch");
        m_inputAcceleration = Input.GetButtonDown("a button");
    }

    void Fly()
    {
        m_pitch = m_inputPitch * (Time.deltaTime * m_rotationSpeed);    // pitch using input
        m_yaw = m_inputYaw * (Time.deltaTime * m_rotationSpeed);        // yaw using input
        m_roll = m_inputYaw * (Time.deltaTime * m_rotationSpeed);       // roll while with yaw input


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

        if (m_inputPitch < -0.9)
        {
            Accelerate();
        }
        else
        {
            Decelerate();
        }

        if (m_speed == m_initialSpeed)
        {
            if (m_inputAcceleration)
            {
                hasComplete = true;
            }
        }


        if(hasComplete)
        {
            Boost();
        }
        else if(hasCompleteDe)
        {
            DeBoost();
        }


        m_rb.velocity = transform.forward * m_speed;
        
        
        Debug.Log(m_rb.velocity.magnitude);
        // create euler angles from the inputs
        m_AddRot.eulerAngles = new Vector3(-m_pitch, -m_yaw, m_roll + m_roll2);

        // add rotation to rigidbody
        m_rb.rotation *= m_AddRot;




    }
    

    void Boost()
    {        

        if (m_speed <= m_speedBoostMax)
        {
            m_speed += m_speedBoost / 4;
        }
        if (m_speed >= m_speedBoostMax)
        {
            hasComplete = false;
            hasCompleteDe = true;
        }
    }

    void DeBoost()
    {
        m_speed -= m_speedBoost / 2;

        if (m_speed < m_initialSpeed)
        {
            m_speed = m_initialSpeed;
            hasCompleteDe = false;
        }
    }

    void Accelerate()
    {
        if (m_speed <= m_maxSpeed)
        {
            m_speed += m_acceleration;
        }
    }

    void Decelerate()
    {
            m_speed -= m_acceleration;

            if (m_speed < m_initialSpeed)
            {
                m_speed = m_initialSpeed;
            }
    }


    void Animate()
    {
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

        if (m_inputYaw < 0)
        {
            m_anim.SetBool("Roll_right", true);
        }
        else
        {
            m_anim.SetBool("Roll_right", false);
        }
    }


}
