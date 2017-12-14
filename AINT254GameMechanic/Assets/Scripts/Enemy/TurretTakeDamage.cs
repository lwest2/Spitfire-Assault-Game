using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Aircraft
{
    [RequireComponent(typeof(playerGUIhelper))]
    public class TurretTakeDamage : MonoBehaviour
    {
        private playerGUIhelper m_playerGUIhelperScript;

        // enemy health set to 100
        private float m_enemyHealth = 100;

        [SerializeField]
        private Slider m_healthSlider;  // health slider

        private bool m_isDead;  // is dead bool

        private void Awake()
        {
            m_playerGUIhelperScript = GameObject.Find("playerGUIHelper").GetComponent<playerGUIhelper>();
        }

        public void setEnemyHealth(float value)
        {
            // deduct enemy health
            m_enemyHealth -= value;
            // update slider
            m_healthSlider.value = m_enemyHealth;

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
            Destroy(gameObject);
            // set player objectives left to -1
            m_playerGUIhelperScript.setPlayerObjectives(-1.0f);
            // play death animation
        }

    }
}