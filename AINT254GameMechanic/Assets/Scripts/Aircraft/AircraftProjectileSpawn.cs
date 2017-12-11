using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftProjectileSpawn : MonoBehaviour {

    private Transform m_transform;

    [SerializeField]
    private GameObject m_crossSection;

	// Use this for initialization
	void Start () {
        m_transform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        m_transform.LookAt(m_crossSection.transform.position);
	}
}
