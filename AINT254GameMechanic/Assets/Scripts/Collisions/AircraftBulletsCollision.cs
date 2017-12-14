using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aircraft
{

    [RequireComponent(typeof(TurretTakeDamage))]
    public class AircraftBulletsCollision : MonoBehaviour
    {
        private TurretTakeDamage m_turretTakeDamageScript;

        private void Awake()
        {
            m_turretTakeDamageScript = GameObject.Find("Ship").GetComponent<TurretTakeDamage>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Map"))
            {
                // play particle splash effect
                gameObject.SetActive(false);
            }

            if (other.CompareTag("Enemy"))
            {
                // play effects
                m_turretTakeDamageScript.setEnemyHealth(0.5f);
            }

            if (other.CompareTag("Projectile"))
            {
                gameObject.SetActive(false);
            }
        }
    }
}