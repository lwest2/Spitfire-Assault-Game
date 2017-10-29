using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 10.0f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Map")
        {
            Destroy(gameObject);
        }
    }
}
