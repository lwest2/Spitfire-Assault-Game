using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aircraft
{
    [RequireComponent(typeof(AircraftInput))]
    public class BarrelRoll : MonoBehaviour
    {
        // references: https://forum.unity.com/threads/barrel-roll.4951/

        private float m_initialJumpSpeed = 0.20f;   // initial speed to rotate aircraft
        private float m_rotateAcceleration = 2.0f;  // rotation acceleration at start of barrel roll
        private float m_rotateDeceleration = 2.0f;  // rotation deceleration towards end of barrel roll
        private float m_rotateSpeed = 300.0f;      // rotation speed of barrel roll
        private float m_minAllowance = 5.0f;      // minimum allowance to stop barrel roll
        private float m_rotateAmount;               // rotation amount, how far it rotates per update frame
        private float m_rotation = 360.00f;         // which rotation is applied
        private float m_curRotate;                  // currant rotation

        private bool m_barrelRollActive = false;    // is barrel roll active
        private bool m_right = false;               // is right bumper in use
        private bool m_left = false;                // is left bumper in use

        private Rigidbody m_rb;             // rigidbody for aircraft

        private bool m_barrelRollInput;    // right barrel roll input

        private Vector3 m_predictUp;    // prediction of the up vector
        private Vector3 m_torqueVector; // how much torque should be added   

        private AircraftInput aircraftInput;

        public void Awake()
        {
            aircraftInput = GetComponent<AircraftInput>();
        }

        void Start()
        {
            m_rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            m_barrelRollInput = aircraftInput.m_barrelRollInput;

            // set variable to local euler angles for z
            m_rotation = m_rb.transform.localEulerAngles.z;

            // predict the up transform
            m_predictUp = Quaternion.AngleAxis(m_rb.angularVelocity.magnitude * Mathf.Rad2Deg * 0.6f / 3.0f, m_rb.angularVelocity) * transform.up;

            // if right bumper active and barrel roll active
            if ((m_left || m_right) && m_barrelRollActive)
            {
                // if rotation is less than the predicted up + 180
                if (m_rotation < m_predictUp.z + 180)
                {
                    // set rotateAmount to initial jump plus rotation to get to 180 with added acceleration
                    m_rotateAmount = Mathf.Clamp01(m_initialJumpSpeed + ((m_rotation / 180) * m_rotateAcceleration));

                }
                else
                {
                    // set rotate amount to decelerate until reaching 360
                    m_rotateAmount = Mathf.Clamp01((((360 - m_rotation) / 180) * m_rotateDeceleration));
                }
            }

            // if barrel roll is active and local z is less than predicted up z or is larger than the predicted up z + 360
            if (m_barrelRollActive && (transform.localEulerAngles.z < m_predictUp.z - m_minAllowance || transform.localEulerAngles.z > (m_predictUp.z + 360) - m_minAllowance))
            {
                // set barrel roll active to false
                m_barrelRollActive = false;
                // set right bool to false
                m_right = false;
                // set left bool to false
                m_left = false;
            }

            // if rotation is less than 360 but more than 270
            if (m_rotation < 360 && m_rotation > 270)
            {
                // and barrelrollinput
                if (m_barrelRollInput)
                {
                    // set active roll for barrel roll right
                    m_barrelRollActive = true;
                    m_right = true;
                    m_left = false;
                }
            }
            // else do the opposite..
            else if (m_rotation > 0 && m_rotation < 90)
            {
                if (m_barrelRollInput)
                {
                    m_barrelRollActive = true;
                    m_left = true;
                    m_right = false;
                }
            }

            // if barrel roll active for right
            if (m_barrelRollActive && m_right)
            {
                // add barrel roll rotations

                ExecuteRoll(-m_curRotate);
            }

            // else do the same for left, opposite variables
            if (m_barrelRollActive && m_left)
            {
                ExecuteRoll(m_curRotate);
            }

        }

        void ExecuteRoll(float curRotateTemp)
        {           
            Debug.Log(m_curRotate);
            m_curRotate = m_rotateAmount * m_rotateSpeed * Time.deltaTime;
            m_rotation = m_curRotate;
            transform.Rotate(Vector3.forward * curRotateTemp);
        }
    }
}