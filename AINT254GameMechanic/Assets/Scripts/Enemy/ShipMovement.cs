using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour {

    // references - http://wiki.unity3d.com/index.php/Wander

    private float m_speed = 10f;
    private float m_directionChange = 10f;
    private float m_timeToDirect = 0.2f;
    private float m_maxHeadingChange = 30;
    private float m_heading;
    Vector3 m_targetRotation;
    private Rigidbody m_rb;

    private void Awake()
    {
        m_heading = Random.Range(0, 360);
        transform.eulerAngles = new Vector3(0, m_heading, 0);

        StartCoroutine(NewHeading());
    }
    // Use this for initialization
    void Start () {
        m_rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, m_targetRotation, Time.deltaTime * m_timeToDirect);
        m_rb.velocity = transform.forward * m_speed;
	}

    // calculates new direction to move towards
    IEnumerator NewHeading()
    {
        while(true)
        {
            NewHeadingDirection();
            yield return new WaitForSeconds(m_directionChange);
        }
    }

    // calculates new direction
    void NewHeadingDirection()
    {
        float floor = Mathf.Clamp(m_heading - m_maxHeadingChange, 0, 360);
        float ceil = Mathf.Clamp(m_heading + m_maxHeadingChange, 0, 360);
        m_heading = Random.Range(floor, ceil);
        m_targetRotation = new Vector3(0, m_heading, 0);
    }
}
