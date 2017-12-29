using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// references: https://www.youtube.com/watch?v=ox-QiHfUqdI

namespace Aircraft
{
    public class TurretTakeDamage : MonoBehaviour
    {
        // enemy health set to 100
        private float m_enemyHealth = 100;

        [SerializeField]
        private Image m_healthSlider;  // health slider

        private bool m_isDead;  // is dead bool
        
        public void setEnemyHealth(float value)
        {
            // deduct enemy health
            m_enemyHealth -= value;

            // update slider
            m_healthSlider.fillAmount = Map(m_enemyHealth, 0, 100, 0, 1);

            // if enemy health is less or equal to 0 and is not dead
            if (m_enemyHealth <= 0 && !m_isDead)
            {
                // call death
                Death();
            }
        }

        void Death()
        {
            // is dead equal to true
            m_isDead = true;
            // destroy turret
            Debug.Log("Disable turret");
        }

        float Map(float value, float inMin, float inMax, float outMin, float outMax)
        {
            return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
            // (current health - deadHealth) * (1 - 0 ratio) / (maximum health - deadHealth) + 0



        }
    }
}