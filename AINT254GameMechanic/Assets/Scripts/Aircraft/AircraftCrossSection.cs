using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftCrossSection : MonoBehaviour {

    [SerializeField]
    private Camera m_camera;

    [SerializeField]
    private Transform m_target;
    

    Vector3 screenPos;
    Vector2 guiPosition;

    private Transform m_transform;

    void Start()
    {
        m_transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update () {
        screenPos = m_camera.WorldToScreenPoint(m_target.position);
        guiPosition = new Vector2(screenPos.x, screenPos.y);

        m_transform.position = guiPosition;
	}
    

}
