using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Aircraft
{

    [RequireComponent(typeof(playerGUIhelper))]
    public class EnemyProjectileCollision : MonoBehaviour
    {
        private playerGUIhelper m_playerGUIhelperScript;

        private void Awake()
        {
            m_playerGUIhelperScript = GameObject.Find("playerGUIHelper").GetComponent<playerGUIhelper>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Map"))
            {
                // play effect
                // disable
                gameObject.SetActive(false);
            }

            if (other.CompareTag("Player"))
            {
                // deduct 5 health from player
                m_playerGUIhelperScript.setPlayerHealth(5f);
            }

            if (other.CompareTag("Enemy"))
            {
                // disable
                gameObject.SetActive(false);
            }
        }
    }
}
