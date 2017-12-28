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
        [SerializeField]
        private GameObject m_target;

        // get enemy projectile
        [SerializeField]
        private GameObject m_bulletPrefab;

        // get turret projectile spawn point
        [SerializeField]
        private Transform[] m_bulletSpawn = new Transform[3];

        private float m_speed = 0.2f;

        // pool manager
        private PoolManager poolManager;

        private bool isFiring = false;

        private Animator m_anim;

        // Use this for initialization
        void Start()
        {
            m_anim = GetComponentInParent<Animator>();
            poolManager = GameObject.Find("PoolManager").GetComponent<PoolManager>();
            m_transform = transform;

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
            if (!isFiring)
            {
                StartCoroutine(Firing());
            }
            
            // instantiate enemy projectile


            Invoke("setFiring", Random.Range(2, 5));
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
