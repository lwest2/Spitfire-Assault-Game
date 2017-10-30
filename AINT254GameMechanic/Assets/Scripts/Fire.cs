using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {


    private float m_inputB_bomb;
    private float m_inputB_bullet;

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
    private bool m_allowBombs = true;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        m_inputB_bomb = Input.GetAxis("left trigger");
        m_inputB_bullet = Input.GetAxis("right trigger");

        if (m_inputB_bomb == 1 && !SwitchCamera.m_cameraSwitcher && m_allowBombs)
        {
            StartCoroutine(Bombs());
        }

        else if (m_inputB_bullet == 1 && SwitchCamera.m_cameraSwitcher && m_allowFire)
        {
            StartCoroutine(Bullets());               
        }
    }

    IEnumerator Bullets()
    {
        m_allowFire = false;
        Instantiate(m_bulletPrefab, m_bulletSpawn.position, m_bulletSpawn.rotation);
        yield return new WaitForSecondsRealtime(0.05f);
        Instantiate(m_bulletPrefab, m_bulletSpawn_2.position, m_bulletSpawn_2.rotation);
        yield return new WaitForSecondsRealtime(0.05f);
        m_allowFire = true;
    }

    IEnumerator Bombs()
    {
        m_allowBombs = false;
        Instantiate(m_bombPrefab, m_bombSpawn.position, m_bombSpawn.rotation);
        yield return new WaitForSecondsRealtime(1.0f);
        m_allowBombs = true;
    }
}
