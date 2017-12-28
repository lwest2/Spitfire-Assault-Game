using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour {
    
    // get transform for gun
    private Transform m_transform;

    // get target for gun to aim at
    [SerializeField]
    private GameObject m_target;

    // get enemy projectile
    [SerializeField]
    private GameObject m_bulletPrefab;

    // get turret projectile spawn point
    [SerializeField]
    private Transform m_bulletSpawn;

    private float m_speed = 0.2f;

    // Use this for initialization
    void Start () {
        m_transform = transform;

        // if target is available
        if (m_target)
        {
            // invoke the repeating fire
            InvokeRepeating("Fire", 1.0f, 3.0f);
        }
        else
        {
            // cancel invoke
            CancelInvoke("Fire");
        }
    }

    void Fire()
    {
        // instantiate enemy projectile
        Instantiate(m_bulletPrefab, m_bulletSpawn.position + transform.forward * 5, m_bulletSpawn.rotation);
    }
}
