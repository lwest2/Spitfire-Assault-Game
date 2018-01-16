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
        private float m_playerObjectives = 2;   // player objects to 3
        public static playerGUIhelper playergui;    // make this script available to other scripts

        [SerializeField]
        private Image m_healthSlider;  // health slider for player

        [SerializeField]
        private Image m_damageImage;    // image for when damaged

        private float m_flashSpeed = 0.01f;  // fade effect
        private Color m_flashColour = new Color(1f, 0f, 0f, 0.1f);  // red transparent image
        private bool m_isDamaged;   // player damaged bool
        private bool m_isDead;      // player dead bool

        [SerializeField]
        private Text m_bombText;    // bombs left text

        [SerializeField]
        private Text m_objectiveText;   // objectives left text

        private SpawnEnemies m_spawnEnemies_script;

        private void Awake()
        {
            m_damageImage = GameObject.Find("DamageImage").GetComponent<Image>();
            m_objectiveText = GameObject.Find("ObjectivesText").GetComponent<Text>();
            m_spawnEnemies_script = GameObject.Find("BuildManager").GetComponent<SpawnEnemies>();
        }

        private void Start()
        {
            m_playerObjectives = m_spawnEnemies_script.getNumberOfShips();
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
            // update slider
            m_healthSlider.fillAmount = Map(m_playerHealth, 0, 100, 0, 1);

            // if player health is less or equal to 0 and is not dead
            if (m_playerHealth <= 0 && !m_isDead)
            {
                // call death
                Death();
            }
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
        
        public void setPlayerObjectives(float shipInterval)
        {
            // if player objectives is more than 0
            if (m_playerObjectives > 0)
            {
                Debug.Log(m_playerObjectives);
                // deduct player objectives
                m_playerObjectives -= shipInterval;
                // update objects left text
                if (m_objectiveText)
                {
                    m_objectiveText.text = "Objectives Left: " + m_playerObjectives.ToString();
                }
                if (m_playerObjectives == 0)
                {
                    
                        // else load scene win
                        Debug.Log("THIS IS PLAYING???");
                        SceneManager.LoadScene(4);
                    
                }
            }
        }

        float Map(float value, float inMin, float inMax, float outMin, float outMax)
        {
            return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
            // (current health - deadHealth) * (1 - 0 ratio) / (maximum health - deadHealth) + 0

        }

    }
}