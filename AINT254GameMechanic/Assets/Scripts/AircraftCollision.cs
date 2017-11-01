using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AircraftCollision : MonoBehaviour {
    

    void OnTriggerEnter(Collider other)
    {
        // if aircraft collides with enemies, or the map
        if (other.tag == "Enemy")
        {
            // load gameover scene
            SceneManager.LoadScene(3);
            
        }

        // if aircraft collides with balloon or crate
        if (other.tag == "Balloon" || other.tag == "Crate")
        {
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Map")
        {
            SceneManager.LoadScene(3);
        }
    }
}
