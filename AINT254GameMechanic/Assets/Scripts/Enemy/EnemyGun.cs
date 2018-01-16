using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aircraft
{
    public class EnemyGun : MonoBehaviour
    {

        // get transform for gun
        private Transform m_transform;

        // get target for gun to aim at
        private GameObject m_target;

        // get enemy projectile
        [SerializeField]
        private GameObject m_bulletPrefab;

        // get turret projectile spawn point
        [SerializeField]
        private Transform[] m_bulletSpawn = new Transform[3];

        private float m_speed;

        // pool manager
        private PoolManager poolManager;

        // is firing default to false
        private bool isFiring = false;

        // gun animation
        private Animator m_anim;

        // will be used for randomly choosing a fire rate for each turret
        // random value
        private int m_randomValue;
        // array of values
        private int[] m_randomValueArray = new int[] { 3, 6, 9 };
        // index
        private int index;

        // audio
        private AudioSource m_audioSource;
        [SerializeField]
        private AudioClip m_audioClip;

        // Use this for initialization
        void Start()
        {
            
            m_anim = GetComponent<Animator>();
            poolManager = GameObject.Find("PoolManager").GetComponent<PoolManager>();
            m_target = GameObject.Find("Air");
            m_audioSource = GetComponent<AudioSource>();
            m_transform = transform;

            // get random value from array and set for index
            index = Random.Range(0, m_randomValueArray.Length);
            m_randomValue = m_randomValueArray[index];

            Debug.Log(m_randomValue);

            // if target is available
            if (m_target)
            {
                // invoke the repeating fire
                InvokeRepeating("Fire", 1.0f, 3.0f);
            }
            else
            {
                // cancel invoke
                CancelInvoke("Fire");
            }
        }

        void Fire()
        {
            if (this)
            {
                if (!isFiring)
                {
                    StartCoroutine(Firing());
                }
            }
            // instantiate enemy projectile


            Invoke("setFiring", m_randomValue);
        }

        void setFiring()
        {
            isFiring = false;
        }

        IEnumerator Firing()
        {
            // play gun animation
            m_anim.Play("Gun");
            // is firing
            isFiring = true;

            // fire projectile to projectile spawn location
            foreach (Transform o in m_bulletSpawn)
            {
                GameObject enemyProjectile = poolManager.GetPooledObject("EnemyProjectile");

                if (enemyProjectile != null)
                {
                    enemyProjectile.transform.position = o.transform.position;
                    enemyProjectile.transform.rotation = o.transform.rotation;
                    enemyProjectile.SetActive(true);
                    m_audioSource.clip = m_audioClip;
                    m_audioSource.PlayOneShot(m_audioSource.clip);
                }

                yield return new WaitForSecondsRealtime(0.5f);
            }
        }

        private void OnDisable()
        {
            CancelInvoke();
        }
    }
}
