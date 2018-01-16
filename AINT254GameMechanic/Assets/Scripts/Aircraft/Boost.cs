using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aircraft
{
    [RequireComponent(typeof(AircraftInput), (typeof(Aircraft)))]
    public class Boost : MonoBehaviour
    {

        private Aircraft aircraftScript;
        private AircraftInput aircraftInput;
        private float m_speed;
        private float m_initialSpeed;
        private float m_speedBoostMax;
        private float m_speedBoost;

        [SerializeField]
        private AudioSource m_audioSource;

        void Awake()
        {
            aircraftScript = GameObject.Find("Air").GetComponent<Aircraft>();
            aircraftInput = GameObject.Find("Air").GetComponent<AircraftInput>();
            m_audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            m_speed = aircraftScript.GetSpeed();
            m_initialSpeed = aircraftScript.GetInitialSpeed();
            m_speedBoost = aircraftScript.getSpeedBoost();

            // if speed is equal to initial constant velocity && input for boost
            if (m_speed == m_initialSpeed && aircraftInput.m_inputAcceleration)
            {
                // set hasComplete to true
                aircraftScript.setCompleteBoost(true);
            }

            // if button is pressed
            if (aircraftScript.getCompleteBoost() == true)
            {
                // boost
                Boosting();
                
            }
            // once reaching max velocity speed this bool will activate deboost
            else if (aircraftScript.getCompleteDeboost() == true)
            {
                // decelerate/deboost
                DeBoosting();
            }
        }

        void Boosting()
        {
            m_speedBoostMax = aircraftScript.getSpeedBoostMax();
            m_audioSource.pitch += 0.015f;
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
                aircraftScript.setCompleteBoost(false);
                // set has complete de to true so it will be able to access the deboost method
                aircraftScript.setCompleteDeboost(true);
            }

            aircraftScript.SetSpeed(m_speed);
        }

        void DeBoosting()
        {
            // deduct speed at fast rate than increasing
            m_speed -= m_speedBoost / 2;
            m_audioSource.pitch -= 0.015f;
            // if speed is less than the initial speed
            if (m_speed < m_initialSpeed)
            {
                m_audioSource.pitch = 1f;
                // set speed to initial speed
                m_speed = m_initialSpeed;
                // has completeDe is now false, will not keep decelerating
                aircraftScript.setCompleteDeboost(false);
            }

            aircraftScript.SetSpeed(m_speed);
        }
    }
}