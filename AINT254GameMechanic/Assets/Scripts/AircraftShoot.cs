using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aircraft
{
    [RequireComponent(typeof(AircraftInput))]
    public class AircraftShoot : MonoBehaviour
    {
        // input
        private AircraftInput aircraftInput;

        // pool manager
        private PoolManager poolManager;

        // AircraftBullet that is pooled
        private GameObject aircraftBullet;

        [SerializeField]
        private GameObject bulletSpawn_1;

        [SerializeField]
        private GameObject bulletSpawn_2;

        public void Awake()
        {
            aircraftInput = GetComponent<AircraftInput>();
            poolManager = GameObject.Find("PoolManager").GetComponent<PoolManager>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void FixedUpdate()
        {
            aircraftBullet = poolManager.GetPooledObject("AircraftBullet");
            aircraftBullet.transform.position = bulletSpawn_1.transform.position;
            aircraftBullet.SetActive(true);
        }
    }
}