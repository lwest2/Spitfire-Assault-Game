using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunRotation : MonoBehaviour {

    // references: https://answers.unity.com/questions/141775/limit-local-rotation.html

    [SerializeField]
    private Transform m_turretBarrels;

    [SerializeField]
    private Transform m_turretBase;

    [SerializeField]
    private Transform m_target;

    private float m_rotSpeed = 0.5f;

    private Quaternion m_lookRot;
    private Vector3 m_dir;

    private Quaternion m_lookRotGun;
    private Vector3 m_gunDir;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        RotateTurretBase();
        RotateTurretBarrels();
    }

    void RotateTurretBase()
    {
        m_dir = (m_target.position - m_turretBase.transform.position).normalized;

        m_dir.y = 0;

        m_lookRot = Quaternion.LookRotation(m_dir.normalized);

        m_turretBase.transform.rotation = Quaternion.Slerp(m_turretBase.transform.rotation, m_lookRot, Time.deltaTime * m_rotSpeed);
       
    }

    void RotateTurretBarrels()
    {
        m_gunDir = (m_target.position - m_turretBarrels.transform.position);

        m_gunDir.z = 0;
        
        m_lookRotGun = Quaternion.LookRotation(m_gunDir.normalized);

        m_turretBarrels.transform.rotation = Quaternion.Slerp(m_turretBarrels.transform.rotation, m_lookRotGun, Time.deltaTime * m_rotSpeed);
        

    }

    private void LateUpdate()
    {
        m_turretBarrels.eulerAngles = new Vector3(ClampAngle(m_turretBarrels.eulerAngles.x, -20, 10), m_turretBase.eulerAngles.y, 0);
    }

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < 90 || angle > 270)
        {       // if angle in the critic region...
            if (angle > 180) angle -= 360;  // convert all angles to -180..+180
            if (max > 180) max -= 360;
            if (min > 180) min -= 360;
        }
        angle = Mathf.Clamp(angle, min, max);
        if (angle < 0) angle += 360;  // if angle negative, convert to 0..360
        return angle;
    }
}
