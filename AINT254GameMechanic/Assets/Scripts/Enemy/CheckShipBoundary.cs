using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aircraft
{
    public class CheckShipBoundary : MonoBehaviour
    {
        // default ship not outside boundary
        private bool outsideBoundary = false;
        // if ship has detected ship boundary default to false
        private bool shipBoundaryDetected = false;
        // other ship from collision detection
        private GameObject m_otherShip;

        // if exiting boundary set booleans respectively
        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "ShipBoundary")
            {
                outsideBoundary = false;
                Debug.Log("IsNowOutsideBoundary");
            }
            if (other.tag == "ShipCollisionDetection")
            {
                shipBoundaryDetected = false;
                Debug.Log("Ship boundary no longer detected");
            }
        }

        // if entering boundary set booleans respectively
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "innerShipBoundary")
            {
                outsideBoundary = true;
                Debug.Log("IsNowInsideBoundary");
            }
            if (other.tag == "ShipCollisionDetection")
            {
                shipBoundaryDetected = true;
                Debug.Log("Ship boundary detected");
            }

            // save collision with ship gameobject
            m_otherShip = other.gameObject;
        }

        public bool getOutsideBoundary()
        {
            return outsideBoundary;
        }

        public bool getShipBoundary()
        {
            return shipBoundaryDetected;
        }

        public GameObject getOtherShip()
        {
            return m_otherShip;
        }
    }
}
