using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aircraft
{

    [RequireComponent(typeof(TurretTakeDamage))]
    public class AircraftBulletsCollision : MonoBehaviour
    {
        private TurretTakeDamage m_turretTakeDamageScript;

        private AudioSource m_audioSource;

        private void Start()
        {
            m_audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Map"))
            {
                // play particle splash effect
                // disable
                gameObject.SetActive(false);
            }

            if (other.CompareTag("Turret"))
            {
                // play effects
                m_turretTakeDamageScript = other.GetComponent<TurretTakeDamage>();
                // deduct 2 health from enemy
                m_turretTakeDamageScript.setEnemyHealth(2.0f);
                // play sound clip for ricochet
                m_audioSource.PlayOneShot(m_audioSource.clip);
            }

            if (other.CompareTag("Projectile"))
            {
                // disable
                gameObject.SetActive(false);
            }
        }
    }
}