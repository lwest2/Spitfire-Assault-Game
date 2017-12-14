using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Aircraft
{
    public class playerGUIhelper : MonoBehaviour
    {
        // references: https://unity3d.com/learn/tutorials/projects/survival-shooter/player-health?playlist=17144

        private float m_playerHealth = 100;     // player health to 100
        private float m_playerBombs = 3;        // player bombs to 3
        private float m_playerObjectives = 1;   // player objects to 3
        public static playerGUIhelper playergui;    // make this script available to other scripts

        [SerializeField]
        private Slider m_healthSlider;  // health slider for player

        [SerializeField]
        private Image m_damageImage;    // image for when damaged

        private float m_flashSpeed = 0.1f;  // fade effect
        private Color m_flashColour = new Color(1f, 0f, 0f, 0.1f);  // red transparent image
        private bool m_isDamaged;   // player damaged bool
        private bool m_isDead;      // player dead bool

        [SerializeField]
        private Text m_bombText;    // bombs left text

        [SerializeField]
        private Text m_objectiveText;   // objectives left text

        private void Awake()
        {
            m_damageImage = GameObject.Find("DamageImage").GetComponent<Image>();
        }

        // Update is called once per frame
        void Update()
        {
            // if player is damaged
            if (m_isDamaged)
            {
                // flash colour
                m_damageImage.color = m_flashColour;
            }
            else
            {
                // else fade back to normal
                m_damageImage.color = Color.Lerp(m_damageImage.color, Color.clear, m_flashSpeed * Time.deltaTime);
            }
            // set is damaged to false
            m_isDamaged = false;
        }

        public void setPlayerHealth(float value)
        {
            // if damaged is true
            m_isDamaged = true;
            // deduct player health
            m_playerHealth -= value;
            // update health slider
            m_healthSlider.value = m_playerHealth;

            // if player health is less or equal to 0 and is not dead
            if (m_playerHealth <= 0 && !m_isDead)
            {
                // call death
                Death();
            }
            Debug.Log(m_playerHealth);
        }

        public float getPlayerHealth()
        {
            return m_playerHealth;
        }

        void Death()
        {
            // is dead is equal to true
            m_isDead = true;
            // load game over scene
            SceneManager.LoadScene(3);
        }

        public void setPlayerBombs(float value)
        {
            // if player bombs is more than 0
            if (m_playerBombs > 0)
            {
                // deduct player bombs
                m_playerBombs += value;
                // update bombs left text
                m_bombText.text = "Bombs: " + m_playerBombs.ToString();
            }
        }

        public void addPlayerBombs(float value)
        {
            if (m_playerBombs >= 0)
            {
                m_playerBombs += value;

                m_bombText.text = "Bombs: " + m_playerBombs.ToString();
            }
        }

        public float getPlayerBombs()
        {
            return m_playerBombs;
        }

        public void setPlayerObjectives(float value)
        {
            // if player objectives is more than 1
            if (m_playerObjectives > 1)
            {
                // deduct player objectives
                m_playerObjectives += value;
                // update objects left text
                m_objectiveText.text = "Objectives Left: " + m_playerObjectives.ToString();
            }
            else
            {
                // else load scene win
                SceneManager.LoadScene(4);
            }
        }

    }
}