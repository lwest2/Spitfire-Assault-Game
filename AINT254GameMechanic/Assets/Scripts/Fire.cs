using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    
    private float m_inputB_bomb;    // input for player bombs
    private float m_inputB_bullet;  // input for player bullets

    [SerializeField]
    private GameObject m_bombPrefab;    // bomb prefab

    [SerializeField]
    private Transform m_bombSpawn;      // spawn point for bombs to drop from

    [SerializeField]
    private GameObject m_bulletPrefab;  // bullet prefab

    [SerializeField]
    private Transform m_bulletSpawn;    // first position for bullets to spawn from

    [SerializeField]
    private Transform m_bulletSpawn_2;  // second position for bullets to spawn from

    private bool m_allowFire = true;    // allow fire set to true
    private bool m_allowBombs = true;   // allow bombs set to true

    private float m_numBombs;           // number of bombs
	
	// Update is called once per frame
	void Update () {
        
        // get input for bomb
        m_inputB_bomb = Input.GetAxis("left trigger");
        // get input for bullet
        m_inputB_bullet = Input.GetAxis("right trigger");

        // get the number of bombs available from the GUIhelper script
        m_numBombs = playerGUIhelper.playergui.getPlayerBombs();

        // if input is true, switiched camera, allow bombs is true, and the number of bombs is more than 0
        if (m_inputB_bomb == 1 && !SwitchCamera.m_cameraSwitcher && m_allowBombs && m_numBombs > 0)
        {
            // start coroutine
            StartCoroutine(Bombs());
        }

        // else if input for bullets is true, and allowed to fire
        else if (m_inputB_bullet == 1 && SwitchCamera.m_cameraSwitcher && m_allowFire)
        {
            // start coroutine
            StartCoroutine(Bullets());               
        }
    }

    IEnumerator Bullets()
    {
        // allow fire to false
        m_allowFire = false;
        // instantiate bullet in bullet spawn 1
        Instantiate(m_bulletPrefab, m_bulletSpawn.position, m_bulletSpawn.rotation);
        // wait
        yield return new WaitForSecondsRealtime(0.05f);
        // instantiate bullet in bullet spawn 2
        Instantiate(m_bulletPrefab, m_bulletSpawn_2.position, m_bulletSpawn_2.rotation);
        // wait
        yield return new WaitForSecondsRealtime(0.05f);
        // allowed to fire again
        m_allowFire = true;
    }

    IEnumerator Bombs()
    {
        // allow bombs to false
        m_allowBombs = false;
        // instante bomb
        Instantiate(m_bombPrefab, m_bombSpawn.position, m_bombSpawn.rotation);
        // set bomb count to -1
        playerGUIhelper.playergui.setPlayerBombs(-1.0f);
        // wait 1 second
        yield return new WaitForSecondsRealtime(1.0f);
        // allow bombs to drop again
        m_allowBombs = true;
    }
}
