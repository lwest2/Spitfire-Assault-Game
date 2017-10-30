using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFacingBillboard : MonoBehaviour {
    // references: http://wiki.unity3d.com/index.php?title=CameraFacingBillboard

    [SerializeField]
    private Camera m_Camera;
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward, Vector3.up);
    }
}
