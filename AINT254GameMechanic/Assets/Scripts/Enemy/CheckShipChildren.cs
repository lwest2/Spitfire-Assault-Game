using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aircraft
{
    public class CheckShipChildren : MonoBehaviour
    {
        private int m_numberOfTurrets = 0;

        public int CheckShip(GameObject ship)
        {
            m_numberOfTurrets = 0;

            Transform[] children = ship.GetComponentsInChildren<Transform>();
            foreach (Transform c in children)
            {
                if (c.CompareTag("Turret"))
                {
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
