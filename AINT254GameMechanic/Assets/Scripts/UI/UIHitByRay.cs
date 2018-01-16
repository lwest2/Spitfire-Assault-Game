using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Aircraft
{
    public class UIHitByRay : MonoBehaviour, IFadeable
    {
        [SerializeField]
        private Canvas m_healthBar;
        private CanvasGroup m_healthBar_CG;
        
        bool fadeOutBool = true;

        void Start()
        {
            m_healthBar_CG = GetComponentInChildren<CanvasGroup>();
            m_healthBar_CG.alpha = 0.4f;
        }

        public void fadeIn()
        {
            Debug.Log("Fade in");

            // fade health bar in
            m_healthBar_CG.alpha = 1f;
            fadeOutBool = true;
        }

        public void fadeOut()
        {
            Debug.Log("Fade out");
            if (fadeOutBool)
            {

                // fade health bar out
                m_healthBar_CG.alpha = 0.4f;
                fadeOutBool = false;
            }
        }
    }
}