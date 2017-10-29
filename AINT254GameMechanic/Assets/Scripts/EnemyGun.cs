using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour {
    
    private Transform m_transform;
    

    [SerializeField]
    private GameObject m_target;

    [SerializeField]
    private GameObject m_bulletPrefab;

    [SerializeField]
    private Transform m_bulletSpawn;
    
    // Use this for initialization
    void Start () {
        m_transform = transform;
        InvokeRepeating("Fire", 1.0f, 3.0f);
    }
	
	// Update is called once per frame
	void Update () {
        m_transform.LookAt(m_target.transform.position);
    }

    void Fire()
    {
        Instantiate(m_bulletPrefab, m_bulletSpawn.position + transform.forward * 5, m_bulletSpawn.rotation);
    }
}
