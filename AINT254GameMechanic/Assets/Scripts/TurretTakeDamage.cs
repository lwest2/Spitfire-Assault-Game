using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretTakeDamage : MonoBehaviour {

    private float m_enemyHealth = 100;
    public TurretTakeDamage turretBehaviour;

    [SerializeField]
    private Slider m_healthSlider;

    private bool m_isDead;

    private void OnEnable()
    {
        turretBehaviour = this;
    }

    public void setEnemyHealth(float value)
    {
        m_enemyHealth += value;
        m_healthSlider.value = m_enemyHealth;

        if (m_enemyHealth <= 0 && !m_isDead)
        {
            Death();
        }
    }

    void Death()
    {
        m_isDead = true;
        Debug.Log("KILL PLAYER HERE");
        Destroy(gameObject);
    }

}
