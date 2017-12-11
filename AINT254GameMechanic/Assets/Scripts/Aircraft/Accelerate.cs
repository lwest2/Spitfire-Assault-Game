using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Aircraft
{
    [RequireComponent(typeof(AircraftInput), (typeof(Aircraft)))]
    public class Accelerate : MonoBehaviour
    {
        private Aircraft aircraftScript;
        private AircraftInput aircraftInput;

        private float m_speed;
        private float m_acceleration;
        private float m_maxSpeed;
        private float m_initialSpeed;

        // Use this for initialization
        void Awake()
        {
            aircraftScript = GetComponent<Aircraft>();
            aircraftInput = GetComponent<AircraftInput>();
        }

        private void FixedUpdate()
        {
            m_speed = aircraftScript.GetSpeed();
            m_maxSpeed = aircraftScript.GetMaxSpeed();
            m_acceleration = aircraftScript.GetAcceleration();
            m_initialSpeed = aircraftScript.GetInitialSpeed();

            // if pitch is decending
            if (aircraftInput.m_inputPitch < -0.9)
            {
                // accelerate aircraft
                Accelerating();
            }
            else
            {
                // decelerate aircraft
                Decelerate();
            }
        }

        void Accelerating()
        {
            // if speed is less or equal to max speed
            if (m_speed <= m_maxSpeed)
            {
                m_speed += m_acceleration;
                // increase speed
                aircraftScript.SetSpeed(m_speed);
            }
        }

        void Decelerate()
        {

            // decrease speed
            m_speed -= m_acceleration;

            // if speed is less than initial speed
            if (m_speed < m_initialSpeed)
            {
                m_speed = m_initialSpeed;

            }

            aircraftScript.SetSpeed(m_speed);
        }
    }
}
