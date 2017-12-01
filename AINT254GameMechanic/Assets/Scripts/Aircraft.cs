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
    private float m_rotationSpeed = 30.0f;  // rotation speed
    private float m_acceleration = 0.025f;  // added acceleration to velocity
    private float m_initialSpeed;           // initial constant speed

    // boost
    private float m_speedBoost = 1f;        // boost speed
    private float m_speedBoostMax = 40f;    // max velocity after boost
    private bool m_hasComplete = false;       // testing if boost has completed
    private bool m_hasCompleteDe = false;     // testing if deboost has completed

    //yaw, pitch, roll
    private Quaternion m_AddRot = Quaternion.identity;  // rotation to add
    private float m_roll = 0;   // roll value
    private float m_pitch = 0;  // pitch value  
    private float m_yaw = 0;    // yaw value

    private Vector3 m_predictUp;    // prediction of the up vector
    private Vector3 m_torqueVector; // how much torque should be added   

    // getter for hasComplete bool
    public bool getComplete()
    {
        return m_hasComplete;
    }

    // getter hasCompleteDe bool
    public bool getCompleteDe()
    {
        return m_hasCompleteDe;
    }

    // Use this for initialization
    void Start () {       
        m_rb = GetComponent<Rigidbody>();
        m_anim = GetComponent<Animator>();
        
        // set initial speed to the initial velocity for later use
        m_initialSpeed = m_speed;
        
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Fly();      // fly
        Animate();  // animate flaps, and rudders
    }

    private void Update()
    {       
        // inputs for xbox controls
        m_inputYaw = Input.GetAxis("yaw");                      // input for yaw
        m_inputPitch = Input.GetAxis("pitch");                  // input for pitch
        m_inputAcceleration = Input.GetButtonDown("a button");  // boost input
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

        // if pitch is decending
        if (m_inputPitch < -0.9)
        {
            // accelerate aircraft
            Accelerate();
        }
        else
        {
            // decelerate aircraft
            Decelerate();
        }

        // if speed is equal to initial constant velocity && input for boost
        if (m_speed == m_initialSpeed && m_inputAcceleration)
        {
            // set hasComplete to true
            m_hasComplete = true;
        }

        // if button is pressed
        if(m_hasComplete)
        {
            // boost
            Boost();
        }
        // once reaching max velocity speed this bool will activate deboost
        else if(m_hasCompleteDe)
        {
            // decelerate/deboost
            DeBoost();
        }

        // set velocity to forward multiplied by the speed
        m_rb.velocity = transform.forward * m_speed;
        
        // create euler angles from the inputs
        m_AddRot.eulerAngles = new Vector3(-m_pitch, -m_yaw, m_roll);

        // add rotation to rigidbody
        m_rb.rotation *= m_AddRot;
    }
    

    void Boost()
    {        
        // if speed is less or equal to max boost speed
        if (m_speed <= m_speedBoostMax)
        {
            // increase speed dramatically
            m_speed += m_speedBoost / 4;
        }
        // else if speed is more or equal to max boost speed
        else if (m_speed >= m_speedBoostMax)
        {
            // set has complete to false so cannot boost twice or more at the same time
            m_hasComplete = false;
            // set has complete de to true so it will be able to access the deboost method
            m_hasCompleteDe = true;
        }
    }

    void DeBoost()
    {
        // deduct speed at fast rate than increasing
        m_speed -= m_speedBoost / 2;

        // if speed is less than the initial speed
        if (m_speed < m_initialSpeed)
        {
            // set speed to initial speed
            m_speed = m_initialSpeed;
            // has completeDe is now false, will not keep decelerating
            m_hasCompleteDe = false;
        }
    }

    void Accelerate()
    {
        // if speed is less or equal to max speed
        if (m_speed <= m_maxSpeed)
        {
            // increase speed
            m_speed += m_acceleration;
        }
    }

    void Decelerate()
    {
         // decrease speed
         m_speed -= m_acceleration;

        // if speed is less than initial speed
        if (m_speed < m_initialSpeed)
        {
            // set speed to initial speed
            m_speed = m_initialSpeed;
        }
    }


    void Animate()
    {
        // animate elevators
        // if pitch is greater than 0
        if (m_inputPitch > 0)
        {
            m_anim.SetBool("Elevate", true);
        }
        else
        {
            m_anim.SetBool("Elevate", false);
        }

        // if pitch is less than 0
        if (m_inputPitch < 0)
        {
            m_anim.SetBool("Deelevate", true);
        }
        else
        {
            m_anim.SetBool("Deelevate", false);
        }
        // end animate elevators

        // animate yaw (rudders, flaps)
        // if yaw is more than 0
        if (m_inputYaw > 0)
        {
            m_anim.SetBool("Roll_left", true);
        }
        else
        {
            m_anim.SetBool("Roll_left", false);
        }

        // if yaw is less than 0
        if (m_inputYaw < 0)
        {
            m_anim.SetBool("Roll_right", true);
        }
        else
        {
            m_anim.SetBool("Roll_right", false);
        }
        // end animate yaw (rudders, flaps)
    }


}
