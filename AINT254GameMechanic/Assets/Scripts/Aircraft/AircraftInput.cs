using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aircraft
{
    public class AircraftInput : MonoBehaviour
    {

        public float m_inputYaw { get; private set; }
        public float m_inputPitch { get; private set; }
        public bool m_inputAcceleration { get; private set; }
        public bool m_barrelRollInput { get; private set; }
        public float m_shootInput { get; private set; }

        // Update is called once per frame
        void Update()
        {
            // controls
            m_inputYaw = -Input.GetAxis("yaw");
            m_inputPitch = Input.GetAxis("pitch");
            m_inputAcceleration = Input.GetButtonDown("a button");
            m_barrelRollInput = Input.GetButtonDown("right bumper");
            m_shootInput = Input.GetAxis("right trigger");
        }
    }
}