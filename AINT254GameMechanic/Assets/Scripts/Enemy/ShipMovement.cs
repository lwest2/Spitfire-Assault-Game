using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aircraft
{
    public class ShipMovement : MonoBehaviour
    {

        // references - http://wiki.unity3d.com/index.php/Wander

        private float m_speed = 10f;
        private float m_directionChange = 10f;
        private float m_timeToDirect = 0.2f;
        private float m_maxHeadingChange = 30;
        private float m_heading;
        Vector3 m_targetRotation;

        private Rigidbody m_rb;

        private Transform shipBoundaryCenter;

        private CheckShipBoundary m_checkShipBoundary_script;

        private Vector3 m_dir;
        private Quaternion m_lookRot;

        private bool m_outsideBoundary;

        private void Awake()
        {

            m_checkShipBoundary_script = GetComponent<CheckShipBoundary>();

            m_heading = Random.Range(0, 360);
            transform.eulerAngles = new Vector3(0, m_heading, 0);

            StartCoroutine(NewHeading());
        }
        // Use this for initialization
        void Start()
        {
            m_rb = GetComponent<Rigidbody>();
            shipBoundaryCenter = GameObject.FindGameObjectWithTag("ShipBoundary").GetComponent<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
            m_outsideBoundary = m_checkShipBoundary_script.getOutsideBoundary();
            Debug.Log(m_outsideBoundary);
            if (m_outsideBoundary)
            {
                transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, m_targetRotation, Time.deltaTime * m_timeToDirect / 10);
                Debug.Log("NoSpecificDirection");
            }
            else
            {
                m_dir = shipBoundaryCenter.position - transform.position; // offset from player

                m_dir.y = 0;

                m_lookRot = Quaternion.LookRotation(m_dir);

                transform.rotation = Quaternion.Slerp(transform.rotation, m_lookRot, Time.deltaTime * m_timeToDirect / 10);


                Debug.Log("FacingCenter");
            }

            m_rb.velocity = transform.forward * m_speed;
        }

        // calculates new direction to move towards
        IEnumerator NewHeading()
        {
            while (true)
            {
                
                    NewHeadingDirection();
                    yield return new WaitForSeconds(m_directionChange);
                
            }
        }

        // calculates new direction
        void NewHeadingDirection()
        {
            if (m_outsideBoundary)
            {
                float floor = Mathf.Clamp(m_heading - m_maxHeadingChange, 0, 360);
                float ceil = Mathf.Clamp(m_heading + m_maxHeadingChange, 0, 360);
                m_heading = Random.Range(floor, ceil);
                Debug.Log(m_heading);
                m_targetRotation = new Vector3(0, m_heading, 0);
            }
            
            
        }

    }
}