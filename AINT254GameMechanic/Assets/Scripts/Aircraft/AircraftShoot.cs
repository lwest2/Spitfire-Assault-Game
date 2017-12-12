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

        [SerializeField]
        private GameObject bulletSpawn_1;
        [SerializeField]
        private GameObject bulletSpawn_2;

        private bool isFiring = false;
        private float fireTime = 0.1f;

        [SerializeField]
        private ParticleSystem m_muzzleFlash;

        [SerializeField]
        private ParticleSystem m_muzzleFlash2;

        void setFiring()
        {
            isFiring = false;
        }


        public void Awake()
        {
            aircraftInput = GetComponent<AircraftInput>();
            poolManager = GameObject.Find("PoolManager").GetComponent<PoolManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if (aircraftInput.m_shootInput > 0)
            {

                if (!isFiring)
                {
                    Fire();
                }

            }

        }

        void Fire()
        {
            isFiring = true;

            StartCoroutine(Firing());
            GameObject bullet = poolManager.GetPooledObject("AircraftBullet");
           
            Invoke("setFiring", fireTime);
        }

        IEnumerator Firing()
        {
            GameObject bullet = poolManager.GetPooledObject("AircraftBullet");

            if (bullet != null)
            {
                bullet.transform.position = bulletSpawn_1.transform.position;
                bullet.transform.rotation = bulletSpawn_1.transform.rotation;
                bullet.SetActive(true);
                m_muzzleFlash.Play();
            }

            yield return new WaitForSecondsRealtime(0.1f);

            m_muzzleFlash.Stop();

            GameObject bullet2 = poolManager.GetPooledObject("AircraftBullet");


            if (bullet2 != null)
            {
                bullet2.transform.position = bulletSpawn_2.transform.position;
                bullet2.transform.rotation = bulletSpawn_2.transform.rotation;
                bullet2.SetActive(true);
                m_muzzleFlash2.Play();
            }

            yield return new WaitForSecondsRealtime(0.1f);
            m_muzzleFlash2.Stop();
        }
    }
}