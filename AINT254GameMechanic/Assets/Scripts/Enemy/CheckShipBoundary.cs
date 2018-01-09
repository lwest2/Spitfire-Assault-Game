using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aircraft
{
    public class CheckShipBoundary : MonoBehaviour
    {

        private bool outsideBoundary = false;

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "ShipBoundary")
            {
                outsideBoundary = false;
                Debug.Log("IsNowOutsideBoundary");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "innerShipBoundary")
            {
                outsideBoundary = true;
                Debug.Log("IsNowInsideBoundary");
            }
        }

        public bool getOutsideBoundary()
        {
            return outsideBoundary;
        }
    }
}
