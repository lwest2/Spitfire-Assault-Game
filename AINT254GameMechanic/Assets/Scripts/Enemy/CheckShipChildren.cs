using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aircraft
{
    public class CheckShipChildren : MonoBehaviour
    {
        // default to 0 (counter)
        private int m_numberOfTurrets = 0;

        public int CheckShip(GameObject ship)
        {
            m_numberOfTurrets = 0;

            // check all children of ship
            Transform[] children = ship.GetComponentsInChildren<Transform>();
            foreach (Transform c in children)
            {
                // if comparing tag is equal to turret
                if (c.CompareTag("Turret"))
                {
                    // is active
                    if (c == isActiveAndEnabled)
                    {
                        m_numberOfTurrets += 1;
                    }
                }

            }
            return m_numberOfTurrets;
        }
    }
}
