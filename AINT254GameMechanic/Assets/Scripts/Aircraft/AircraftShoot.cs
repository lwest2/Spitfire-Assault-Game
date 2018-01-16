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
        
        private AudioSource m_audioSource;
        


        void setFiring()
        {
            // can now fire again
            isFiring = false;
        }


        public void Awake()
        {
            aircraftInput = GetComponent<AircraftInput>();
            poolManager = GameObject.Find("PoolManager").GetComponent<PoolManager>();
            m_audioSource = GameObject.Find("Aircraft").GetComponent<AudioSource>();

        }

        // Update is called once per frame
        void Update()
        {
            // if trying to shoot
            if (aircraftInput.m_shootInput > 0)
            {
                // is not firing already
                if (!isFiring)
                {
                    // fire
                    Fire();
                }

            }
            else
            {
                // else stop muzzle flashes
                m_muzzleFlash.Stop();
                m_muzzleFlash2.Stop();
            }

        }

        void Fire()
        {
            // firing now true
            isFiring = true;

            StartCoroutine(Firing());
            // fire rate helper
            Invoke("setFiring", fireTime);
        }

        IEnumerator Firing()
        {
            // get bullet from pool manager
            GameObject bullet = poolManager.GetPooledObject("AircraftBullet");

            if (bullet != null)
            {   
                // set bullet rotation and transform to the bullet spawns
                bullet.transform.position = bulletSpawn_1.transform.position;
                bullet.transform.rotation = bulletSpawn_1.transform.rotation;
                // muzzle flash
                m_muzzleFlash.Play();
                
                // gun sound
                m_audioSource.Play();
                bullet.SetActive(true);
            }

            yield return new WaitForSecondsRealtime(0.1f);

            // repeated for other spawn
            GameObject bullet2 = poolManager.GetPooledObject("AircraftBullet");

            
            if (bullet2 != null)
            {             
                bullet2.transform.position = bulletSpawn_2.transform.position;
                bullet2.transform.rotation = bulletSpawn_2.transform.rotation;

                m_audioSource.Play();
                m_muzzleFlash2.Play();
                bullet2.SetActive(true);
            }

            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
}