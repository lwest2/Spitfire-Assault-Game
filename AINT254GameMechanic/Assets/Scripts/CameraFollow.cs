// Smooth Follow from Standard Assets
// Converted to C# because I fucking hate UnityScript and it's inexistant C# interoperability
// If you have C# code and you want to edit SmoothFollow's vars ingame, use this instead.
using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    // references: https://gist.github.com/ftvs/5822103

    private Camera m_cameraFOV;

    private float m_inputPitch; // input for pitch
    private float m_inputAcceleration;   // input for acceleration

    // How long the object should shake for.
    private float m_shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    private float m_shakeAmount = 0.07f;
    private float m_decreaseFactor = 2f;

    // field of view
    private float m_fieldOfView = 5.0f;
    private float m_initialFOV;
    private float m_maxFOV = 80.0f;

    // The target we are following
    public Transform target;
    // The distance in the x-z plane to the target
    public float distance = 10.0f;
    // the height we want the camera to be above the target
    public float height = 5.0f;
    // How much we 
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;

    // Place the script in the Camera-Control group in the component menu
    [AddComponentMenu("Camera-Control/Smooth Follow")]

    

    // Place the script in the Camera-Control group in the component menu
    private void Start()
    {
        m_cameraFOV = GetComponent<Camera>();
        m_initialFOV = m_cameraFOV.fieldOfView;
    }

    void FixedUpdate()
    {
        // Early out if we don't have a target
        if (!target) return;

        // Calculate the current rotation angles
        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;

        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * distance;

        // Set the height of the camera
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        // Always look at the target
        transform.LookAt(target);

        // shake camera
        ShakeCamera();
    }

    void Update()
    {
        m_inputPitch = Input.GetAxis("pitch");
        m_inputAcceleration = Input.GetAxis("acceleration");
    }

    void ShakeCamera()
    {
        if (m_inputPitch < -0.9 || m_inputAcceleration < -0.9)
        {
            if (m_cameraFOV.fieldOfView < m_maxFOV)
            {
                transform.position = transform.position + Random.insideUnitSphere * m_shakeAmount;

                m_shakeDuration += Time.deltaTime * m_decreaseFactor;
                ChangeFOV();
            }
        }
        else
        {
            InitialFOV();
        }
    }

    void ChangeFOV()
    {
        m_cameraFOV.fieldOfView = m_cameraFOV.fieldOfView + m_fieldOfView * Time.deltaTime;
    }

    void InitialFOV()
    {
        if (m_cameraFOV.fieldOfView > m_initialFOV)
        {
            m_cameraFOV.fieldOfView = m_cameraFOV.fieldOfView - m_fieldOfView * Time.deltaTime;
        }
    }
}