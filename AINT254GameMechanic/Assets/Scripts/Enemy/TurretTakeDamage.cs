using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
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

        private SpawnEnemies m_spawnEnemies_script;
        private List<GameObject> shipList = new List<GameObject>();
        private CheckShipChildren m_checkShipChidren_script;
        private playerGUIhelper m_playerGUIhelperScript;

        private void Awake()
        {
            m_playerGUIhelperScript = GameObject.Find("playerGUIHelper").GetComponent<playerGUIhelper>();
            m_spawnEnemies_script = GameObject.Find("BuildManager").GetComponent<SpawnEnemies>();
            shipList = m_spawnEnemies_script.getShipList();
        }

        public void setEnemyHealth(float value)
        {
            // deduct enemy health
            m_enemyHealth -= value;

            // update slider
            m_healthSlider.fillAmount = Map(m_enemyHealth, 0, 100, 0, 1);

            // if enemy health is less or equal to 0 and is not dead
            if (m_enemyHealth <= 0)
            {
                // call death
                Death();
            }
        }

        void Death()
        {
            // destroy turret
            gameObject.SetActive(false);

            CheckShip();

        }

        float Map(float value, float inMin, float inMax, float outMin, float outMax)
        {
            return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
            // (current health - deadHealth) * (1 - 0 ratio) / (maximum health - deadHealth) + 0

        }

        void CheckShip()
        {
            // for every ship alive
            foreach (GameObject ship in shipList.ToList())
            {
                // check children
                m_checkShipChidren_script = ship.GetComponent<CheckShipChildren>();

                // number of turrets on the ship
                int numberOfTurrets = m_checkShipChidren_script.CheckShip(ship);

                // if number of turrets equal to 0
                if (numberOfTurrets == 0)
                {
                    // destroy ship and set player objectives to -1
                    Destroy(ship);
                    shipList.Remove(ship);
                    m_playerGUIhelperScript.setPlayerObjectives(1.0f);

                }
            }
        }
    }
}