using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFacingBillboard : MonoBehaviour {
    // references: http://wiki.unity3d.com/index.php?title=CameraFacingBillboard

    // get reference for main camera
    [SerializeField]
    private Camera m_Camera;

    private void Start()
    {
        m_Camera = Camera.main;
    }

    // Update is called once per frame
    void Update () {
        // get the healthbars to look at the direction of the camera and its rotation
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward, Vector3.up);
    }
}
