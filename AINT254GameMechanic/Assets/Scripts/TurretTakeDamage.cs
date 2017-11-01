using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretTakeDamage : MonoBehaviour {

    // enemy health set to 100
    private float m_enemyHealth = 100;
    public TurretTakeDamage turretBehaviour;

    [SerializeField]
    private Slider m_healthSlider;  // health slider

    private bool m_isDead;  // is dead bool

    private void OnEnable()
    {
        // instance of turret to this object
        turretBehaviour = this;
    }

    public void setEnemyHealth(float value)
    {
        // deduct enemy health
        m_enemyHealth += value;
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
        playerGUIhelper.playergui.setPlayerObjectives(-1.0f);
    }

}
