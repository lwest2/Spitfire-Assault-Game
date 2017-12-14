using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToTarget : MonoBehaviour {

    [SerializeField]
    private Transform m_target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (m_target != null)
        {

            transform.position = m_target.transform.position;
        }
	}
}
