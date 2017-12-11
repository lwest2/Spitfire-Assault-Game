using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aircraft
{
    [RequireComponent(typeof(AircraftInput))]
    public class AnimateAircraft : MonoBehaviour
    {

        private AircraftInput aircraftInput;
        private Animator m_anim;

        // Use this for initialization
        void Start()
        {
            aircraftInput = GetComponent<AircraftInput>();
            m_anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            // animate elevators
            // if pitch is greater than 0
            if (aircraftInput.m_inputPitch > 0)
            {
                m_anim.SetBool("Elevate", true);
            }
            else
            {
                m_anim.SetBool("Elevate", false);
            }

            // if pitch is less than 0
            if (aircraftInput.m_inputPitch < 0)
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
            if (aircraftInput.m_inputYaw > 0)
            {
                m_anim.SetBool("Roll_left", true);
            }
            else
            {
                m_anim.SetBool("Roll_left", false);
            }

            // if yaw is less than 0
            if (aircraftInput.m_inputYaw < 0)
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
}