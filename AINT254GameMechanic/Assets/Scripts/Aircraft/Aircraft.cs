using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aircraft
{
    [RequireComponent(typeof(AircraftInput))]
    public class Aircraft : MonoBehaviour
    {

        // references: https://keithmaggio.wordpress.com/2011/07/01/unity-3d-code-snippet-flight-script/
        // http://answers.unity3d.com/questions/554291/finding-a-rigidbodys-rotation-speed-and-direction.html
        // http://answers.unity3d.com/questions/10425/how-to-stabilize-angular-motion-alignment-of-hover.html
        // https://answers.unity.com/questions/1052724/how-to-have-a-speed-boost-in-my-game.html

        private Rigidbody m_rb;             // rigidbody for aircraft

        // input
        private AircraftInput aircraftInput;

        // acceleration and decelleration
        private float m_speed = 25.0f;          // starting speed
        private float m_maxSpeed = 28.0f;       // max velocity
        private float m_rotationSpeed = 30.0f;  // rotation speed
        private float m_acceleration = 0.025f;  // added acceleration to velocity
        private float m_initialSpeed;           // initial constant speed

        // boost
        private float m_speedBoost = 1f;        // boost speed
        private float m_speedBoostMax = 40f;    // max velocity after boost

        private bool m_hasCompleteBoost = false;       // testing if boost has completed
        private bool m_hasCompleteDeBoost = false;     // testing if deboost has completed

        //yaw, pitch, roll
        private Quaternion m_AddRot = Quaternion.identity;  // rotation to add
        private float m_roll = 0;   // roll value
        private float m_pitch = 0;  // pitch value  
        private float m_yaw = 0;    // yaw value

        private Vector3 m_predictUp;    // prediction of the up vector
        private Vector3 m_torqueVector; // how much torque should be added   

        public void Awake()
        {
            aircraftInput = GetComponent<AircraftInput>();
        }

        public void SetSpeed(float m_speed)
        {
            this.m_speed = m_speed;
        }

        public float GetSpeed()
        {
            return m_speed;
        }

        public float GetInitialSpeed()
        {
            return m_initialSpeed;
        }

        public float GetMaxSpeed()
        {
            return m_maxSpeed;
        }

        public float GetAcceleration()
        {
            return m_acceleration;
        }

        // getter for hasCompleteBoost bool
        public bool getCompleteBoost()
        {
            return m_hasCompleteBoost;
        }

        public void setCompleteBoost(bool hasCompleteBoost)
        {
            this.m_hasCompleteBoost = hasCompleteBoost;
        }

        // getter hasCompleteDeboost bool
        public bool getCompleteDeboost()
        {
            return m_hasCompleteDeBoost;
        }

        public void setCompleteDeboost(bool hasCompleteDeboost)
        {
            this.m_hasCompleteDeBoost = hasCompleteDeboost;
        }

        public float getSpeedBoostMax()
        {
            return m_speedBoostMax;
        }

        public float getSpeedBoost()
        {
            return m_speedBoost;
        }

        // Use this for initialization
        void Start()
        {
            m_rb = GetComponent<Rigidbody>();

            // set initial speed to the initial velocity for later use
            m_initialSpeed = m_speed;

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Fly();      // fly
        }


        void Fly()
        {
            m_pitch = aircraftInput.m_inputPitch * (Time.deltaTime * m_rotationSpeed);    // pitch using input
            m_yaw = aircraftInput.m_inputYaw * (Time.deltaTime * m_rotationSpeed);        // yaw using input
            m_roll = aircraftInput.m_inputYaw * (Time.deltaTime * m_rotationSpeed);       // roll while with yaw input

            // predict the up transform
            m_predictUp = Quaternion.AngleAxis(m_rb.angularVelocity.magnitude * Mathf.Rad2Deg * 0.6f / 3.0f, m_rb.angularVelocity) * transform.up;
            // get the cross vector between the predicted up transformation and the current up transformation
            m_torqueVector = Vector3.Cross(m_predictUp, Vector3.up);
            // add torque
            m_rb.AddTorque(m_torqueVector * 1.0f * 1.0f);

            // set velocity to forward multiplied by the speed
            m_rb.velocity = transform.forward * m_speed;

            // create euler angles from the inputs
            m_AddRot.eulerAngles = new Vector3(-m_pitch, -m_yaw, m_roll);

            // add rotation to rigidbody
            m_rb.rotation *= m_AddRot;
        }









    }
}