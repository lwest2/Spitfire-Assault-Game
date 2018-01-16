using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aircraft
{
    public class EnemyGunRotation : MonoBehaviour
    {

        // references: https://answers.unity.com/questions/141775/limit-local-rotation.html

        [SerializeField]
        private Transform m_turretBarrels;

        [SerializeField]
        private Transform m_turretBase;

        private Transform m_target;

        private float m_rotSpeed;

        private Quaternion m_lookRot;
        private Vector3 m_dir;

        private Quaternion m_lookRotGun;
        private Vector3 m_gunDir;

        private Clamp m_clampScript;

        private void Awake()
        {
            m_clampScript = GameObject.Find("Helper").GetComponent<Clamp>();
        }

        // Use this for initialization
        void Start()
        {
            m_target = GameObject.Find("Air").GetComponent<Transform>();
            // random rotation speed
            m_rotSpeed = Random.Range(0.8f, 0.10f);
        }

        // Update is called once per frame
        void Update()
        {
            RotateTurretBase();
            RotateTurretBarrels();
        }

        void RotateTurretBase()
        {
            // rotating turret base along horizontal axis
            m_dir = m_target.position - m_turretBase.transform.position;  // check direction of aircraft

            m_dir.y = 0; // y position does not matter when facing horizontal

            m_lookRot = Quaternion.LookRotation(m_dir); // get the quaternion for looking towards the angle

            m_turretBase.transform.rotation = Quaternion.Slerp(m_turretBase.transform.rotation, m_lookRot, Time.deltaTime * m_rotSpeed); // slerp towards that quaternion
        }

        void RotateTurretBarrels()
        {
            // rotating turret barrels along vertical axis
            m_gunDir = (m_target.position - m_turretBarrels.transform.position); // check direction of aircraft

            m_gunDir.z = 0; // z position does not matter

            m_lookRotGun = Quaternion.LookRotation(m_gunDir.normalized); // get quaternion for looking towards angle

            m_turretBarrels.transform.rotation = Quaternion.Lerp(m_turretBarrels.transform.rotation, m_lookRotGun, Time.deltaTime * m_rotSpeed); // lerp the rotation
        }

        private void LateUpdate()
        {
            m_turretBarrels.eulerAngles = new Vector3(m_clampScript.ClampAngle(m_turretBarrels.eulerAngles.x, -10, 10), m_turretBase.eulerAngles.y, 0); // clamp the angle of the barrels so that they do not extend the base turrets gun holes
        }
    }
}