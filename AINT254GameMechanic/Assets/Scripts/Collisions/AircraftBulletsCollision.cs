using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aircraft
{

    [RequireComponent(typeof(TurretTakeDamage))]
    public class AircraftBulletsCollision : MonoBehaviour
    {
        private TurretTakeDamage m_turretTakeDamageScript;


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Map"))
            {
                // play particle splash effect
                gameObject.SetActive(false);
            }

            if (other.CompareTag("Turret"))
            {
                // play effects
                m_turretTakeDamageScript = other.GetComponent<TurretTakeDamage>();
                m_turretTakeDamageScript.setEnemyHealth(2.0f);
            }

            if (other.CompareTag("Projectile"))
            {
                gameObject.SetActive(false);
            }
        }
    }
}