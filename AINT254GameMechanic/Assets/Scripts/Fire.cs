using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {


    private bool m_inputB_bomb;
    private bool m_inputB_bullet;

    [SerializeField]
    private GameObject m_bombPrefab;

    [SerializeField]
    private Transform m_bombSpawn;

    [SerializeField]
    private GameObject m_bulletPrefab;

    [SerializeField]
    private Transform m_bulletSpawn;

    [SerializeField]
    private Transform m_bulletSpawn_2;

    private bool m_allowFire = true;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        m_inputB_bomb = Input.GetKeyDown("joystick button 1");
        m_inputB_bullet = Input.GetKey("joystick button 1");

        if (m_inputB_bomb && !SwitchCamera.m_cameraSwitcher)
        {
            StartCoroutine(Bombs());
        }

        else if (m_inputB_bullet && SwitchCamera.m_cameraSwitcher && m_allowFire)
        {
            StartCoroutine(Bullets());               
        }
    }

    IEnumerator Bullets()
    {
        m_allowFire = false;
        Instantiate(m_bulletPrefab, m_bulletSpawn.position, m_bulletSpawn.rotation);
        yield return new WaitForSecondsRealtime(0.15f);
        Instantiate(m_bulletPrefab, m_bulletSpawn_2.position, m_bulletSpawn_2.rotation);
        yield return new WaitForSecondsRealtime(0.15f);
        m_allowFire = true;
    }

    IEnumerator Bombs()
    {
        Instantiate(m_bombPrefab, m_bombSpawn.position, m_bombSpawn.rotation);
        yield return new WaitForSecondsRealtime(1.0f);
    }
}
