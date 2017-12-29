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
            Transform[] m_turretSpawn = GetComponentsInChildren<Transform>();
            foreach (Transform child in m_turretSpawn)
            {
                if (child.CompareTag("EnemyTurretSpawn"))
                {
                    GameObject temp = Instantiate(turretPrefab, child);
                    turretList.Add(temp);
                }
            }
        }
    }
}