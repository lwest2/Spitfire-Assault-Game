using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Aircraft
{
    public class OnDisableMe : MonoBehaviour
    {

        private playerGUIhelper m_playerGUIhelperScript;

        private void Awake()
        {
            m_playerGUIhelperScript = GameObject.Find("playerGUIHelper").GetComponent<playerGUIhelper>();
        }

        private void OnDisable()
        {
            
            m_playerGUIhelperScript.setPlayerObjectives(1.0f);
        }
        
    }
}