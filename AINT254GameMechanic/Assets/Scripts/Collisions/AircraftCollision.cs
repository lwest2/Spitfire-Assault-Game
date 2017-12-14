using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AircraftCollision : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Map"))
        {
            // play death animation (drown)
            SceneManager.LoadScene(3);
        }

        if(other.CompareTag("Enemy"))
        {
            // play death animation (explode)
            SceneManager.LoadScene(3);
        }
    }

}
