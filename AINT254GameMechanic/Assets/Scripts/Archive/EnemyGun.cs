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

        private bool isFiring = false;

        private Animator m_anim;

        private int m_randomValue;
        private int[] m_randomValueArray = new int[] { 3, 6, 9 };
        private int index;

        // Use this for initialization
        void Start()
        {
            
            m_anim = GetComponent<Animator>();
            poolManager = GameObject.Find("PoolManager").GetComponent<PoolManager>();
            m_target = GameObject.Find("Air");

            m_transform = transform;

            index = Random.Range(0, m_randomValueArray.Length);
            m_randomValue = m_randomValueArray[index];

            Debug.Log(m_randomValue);

            // if target is available
            if (m_target)
            {
                Debug.Log("Test");
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
            if (!isFiring)
            {
                StartCoroutine(Firing());
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
            m_anim.Play("Gun");
            isFiring = true;

            foreach (Transform o in m_bulletSpawn)
            {
                GameObject enemyProjectile = poolManager.GetPooledObject("EnemyProjectile");

                if (enemyProjectile != null)
                {
                    enemyProjectile.transform.position = o.transform.position;
                    enemyProjectile.transform.rotation = o.transform.rotation;
                    enemyProjectile.SetActive(true);
                }

                yield return new WaitForSecondsRealtime(0.5f);
            }
        }
    }
}
