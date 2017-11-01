using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCrates : MonoBehaviour {

    [SerializeField]
    private GameObject m_crate; // crate prefab
    private Vector3 m_position; // position
    
	// Use this for initialization
	void Start () {
        InvokeRepeating("SpawnCrate", 3.0f, 20.0f); // invoke method repeating every 20 seconds
	}
	

    void SpawnCrate()
    {
        // get a random position within the map
        m_position = new Vector3(Random.Range(-100.0f, 80.0f), Random.Range(9.0f, 15.0f), Random.Range(-33.0f, 300.0f));
        // create an object
        GameObject temporary = Instantiate(m_crate, m_position, Quaternion.identity) as GameObject;        
        // destroy in 20 seconds      
        Destroy(temporary, 20.0f);
    }
}
