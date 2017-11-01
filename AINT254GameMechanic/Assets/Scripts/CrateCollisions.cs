using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateCollisions : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        // if crate collides with map or enemy
        if (collision.collider.tag == "Enemy" || collision.collider.tag == "Map" || collision.collider.tag == "Projectile")
        {
            Destroy(gameObject);
            Debug.Log("Destroy");
        }
    }
}
