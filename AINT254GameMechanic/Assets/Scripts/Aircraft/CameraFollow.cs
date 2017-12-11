// Smooth Follow from Standard Assets
// Converted to C# because I fucking hate UnityScript and it's inexistant C# interoperability
// If you have C# code and you want to edit SmoothFollow's vars ingame, use this instead.
using UnityEngine;
using System.Collections;
namespace Aircraft
{
    [RequireComponent(typeof(AircraftInput), (typeof(Aircraft)))]
    public class CameraFollow : MonoBehaviour
    {
        // references: https://gist.github.com/ftvs/5822103

        private Camera m_cameraFOV;

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
        [SerializeField]
        private Transform m_target;
        // The distance in the x-z plane to the target
        [SerializeField]
        private float m_distance = 10.0f;
        // the height we want the camera to be above the target
        [SerializeField]
        private float m_height = 5.0f;
        // How much we dampen height and rotation
        [SerializeField]
        private float m_heightDamping = 2.0f;
        [SerializeField]
        private float m_rotationDamping = 3.0f;

        // Place the script in the Camera-Control group in the component menu
        [AddComponentMenu("Camera-Control/Smooth Follow")]

        private Aircraft aircraftScript;
        private AircraftInput aircraftInput;

        void Awake()
        {
            aircraftScript = GameObject.Find("Air").GetComponent<Aircraft>();         
            aircraftInput = GameObject.Find("Air").GetComponent<AircraftInput>();
        }

        // Place the script in the Camera-Control group in the component menu
        void Start()
        {
            m_cameraFOV = GetComponent<Camera>();
            // get intial FOV
            m_initialFOV = m_cameraFOV.fieldOfView;
        }

        void FixedUpdate()
        {
            // Early out if we don't have a target
            if (!m_target) return;

            // Calculate the current rotation angles
            float wantedRotationAngle = m_target.eulerAngles.y;
            float wantedHeight = m_target.position.y + m_height;

            float currentRotationAngle = transform.eulerAngles.y;
            float currentHeight = transform.position.y;

            // Damp the rotation around the y-axis
            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, m_rotationDamping * Time.deltaTime);

            // Damp the height
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, m_heightDamping * Time.deltaTime);

            // Convert the angle into a rotation
            var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

            // Set the position of the camera on the x-z plane to:
            // distance meters behind the target
            transform.position = m_target.position;
            transform.position -= currentRotation * Vector3.forward * m_distance;

            // Set the height of the camera
            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

            // Always look at the target
            transform.LookAt(m_target);

            // shake camera
            ShakeCamera();
        }

        void ShakeCamera()
        {
            // if pitch is decending or aircraft hascomplete bool (boosting)
            if (aircraftInput.m_inputPitch < -0.9 || aircraftScript.getCompleteBoost())
            {
                // if FOV is less than max FOV
                if (m_cameraFOV.fieldOfView < m_maxFOV)
                {
                    // shake screen within parameters of a sphere multiplied by shake amount
                    transform.position = transform.position + Random.insideUnitSphere * m_shakeAmount;

                    // decrease shake over time
                    m_shakeDuration += Time.deltaTime * m_decreaseFactor;

                    // change FOV
                    ChangeFOV();
                }
            }
            else
            {
                // else use initial FOV
                InitialFOV();
            }
        }

        void ChangeFOV()
        {
            // increase FOV over time
            m_cameraFOV.fieldOfView = m_cameraFOV.fieldOfView + m_fieldOfView * Time.deltaTime;
        }

        void InitialFOV()
        {
            // if FOV is more than initial FOV
            if (m_cameraFOV.fieldOfView > m_initialFOV)
            {
                // decrease FOV over time
                m_cameraFOV.fieldOfView = m_cameraFOV.fieldOfView - m_fieldOfView * Time.deltaTime;
            }
        }
    }
}