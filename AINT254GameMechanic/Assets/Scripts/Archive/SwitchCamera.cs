using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    // input button Y
    private bool m_inputY;
    // camera switch bool set to true, available for other classes
    public static bool m_cameraSwitcher = true;

    [SerializeField]
    private Animator m_cameraMovement;

    // Use this for initialization
    void Start()
    {
        // get animation
        m_cameraMovement = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // button Y is pressed value
        m_inputY = Input.GetKeyDown("joystick button 3");


        // if Y is true
       if (m_inputY)
        {
            // if camera switcher is true
            if (m_cameraSwitcher)
            {
                // move to other camera angle
                m_cameraMovement.Play("CameraAnimation");
                
            }
            else
            {
                // else switch back to the other camera angle
                m_cameraMovement.Play("CameraAnimation_B");
            }
            
            // invert camera switcher bool
            m_cameraSwitcher = !m_cameraSwitcher;
        }

        
    }

    public bool getCameraSwitcher()
    {
        return m_cameraSwitcher;
    }
}
