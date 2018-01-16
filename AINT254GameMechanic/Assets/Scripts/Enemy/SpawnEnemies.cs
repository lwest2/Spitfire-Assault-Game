using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aircraft
{
    public class SpawnEnemies : MonoBehaviour
    {

        [SerializeField]
        private GameObject turretPrefab;

        [SerializeField]
        private GameObject shipPrefab;

        [SerializeField]
        private GameObject shipSpawnPrefab;

        private List<GameObject> turretList = new List<GameObject>();
        private List<GameObject> shipList = new List<GameObject>();
        private List<GameObject> shipSpawnList = new List<GameObject>();

        private Vector3 position;
        private float shipNumber = 2;
        private float[] randomNumberArray = new float[4] { 200, 400, 600, 800 };

        // Use this for initialization
        void Start()
        {
            BuildShipSpawnPoints();
            BuildShip();
            BuildTurrets();
        }

        void BuildShipSpawnPoints()
        {
            // randomly place spawn points for ships
            for (int i = 0; i < shipNumber; i++)
            {
                position = new Vector3(randomNumberArray[i], 27, randomNumberArray[i]);
                GameObject shipSpawn = Instantiate(shipSpawnPrefab, position, Quaternion.identity);
                shipSpawnList.Add(shipSpawn);
            }
        }

        void BuildShip()
        {
            // build ships on spawn locations
            foreach (GameObject shipspawn in shipSpawnList)
            {
                GameObject ship = Instantiate(shipPrefab, shipspawn.transform);
                shipList.Add(ship);
            }
        }

        void BuildTurrets()
        {
            // for each ship
            foreach (GameObject ship in shipList)
            {
                // get turret spawn in ship
                Transform[] m_turretSpawn = ship.GetComponentsInChildren<Transform>();
                foreach (Transform child in m_turretSpawn)
                {
                    if (child.CompareTag("EnemyTurretSpawn"))
                    {
                        // instantiate at position of turret spawn
                        GameObject temp = Instantiate(turretPrefab, child) as GameObject;
                        turretList.Add(temp);
                    }
                }
            }
        }

        public List<GameObject> getTurretList()
        {
            return turretList;
        }

        public List<GameObject> getShipList()
        {
            return shipList;
        }

        public int getNumberOfShips()
        {
            return NumberOfShips();
        }

        private int NumberOfShips()
        {
            int numberOfShips;
            numberOfShips = shipList.Count;
            return numberOfShips;
        }
    }
}