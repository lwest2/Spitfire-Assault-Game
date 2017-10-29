using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{

    private bool m_inputY;
    public static bool m_cameraSwitcher = true;
    private Texture2D texture;

    [SerializeField]
    private Animator m_cameraMovement;

    // Use this for initialization
    void Start()
    {
        m_cameraMovement = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        m_inputY = Input.GetKeyDown("joystick button 3");

       if (m_inputY)
        {
            if (m_cameraSwitcher)
            {
                m_cameraMovement.Play("CameraAnimation");
                
            }
            else
            {
                m_cameraMovement.Play("CameraAnimation_B");
            }
            m_cameraSwitcher = !m_cameraSwitcher;
        }

        
    }

    public bool getCameraSwitcher()
    {
        return m_cameraSwitcher;
    }
}
