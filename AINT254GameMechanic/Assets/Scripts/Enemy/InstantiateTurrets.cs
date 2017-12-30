using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aircraft
{
    public class InstantiateTurrets : MonoBehaviour
    {

        [SerializeField]
        private GameObject turretPrefab;

        private List<GameObject> turretList = new List<GameObject>();

        // Use this for initialization
        void Start()
        {

        }
        
    }
}