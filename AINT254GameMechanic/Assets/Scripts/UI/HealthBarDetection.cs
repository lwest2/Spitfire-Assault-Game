using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aircraft {
    public class HealthBarDetection : MonoBehaviour {

        private Transform m_transform;
        private SpawnEnemies m_spawnEnemiesScript;

        void Start()
        {
            m_transform = GetComponent<Transform>();
            m_spawnEnemiesScript = GameObject.Find("BuildManager").GetComponent<SpawnEnemies>();


        }

        // Update is called once per frame
        void Update() {
            // create raycast
            RaycastHit hit;
            // fire raycast
            if (Physics.Raycast(m_transform.position, m_transform.forward, out hit))
            {
                // for each object turret
                
                foreach (GameObject o in m_spawnEnemiesScript.getTurretList())
                {
                    IFadeable enemy = o.GetComponent<UIHitByRay>();
                    // if turret in list index is the turret that is hit
                    if (o == hit.collider.gameObject)
                    {
                        enemy.fadeIn();
                    }
                    else
                    {
                        enemy.fadeOut();
                    }
                }
                
            }
        }
    }
}