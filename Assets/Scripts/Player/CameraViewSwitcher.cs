using UnityEngine;

public class CameraViewSwitcher : MonoBehaviour
{
    [Header("Cinemachine Cameras")]
    [SerializeField] private GameObject firstPersonCamera;
    [SerializeField] private GameObject thirdPersonCamera;

    [Header("View Targets")]
    [SerializeField] private Transform thirdPersonViewPoint;

    [Header("Optional Visuals")]
    [SerializeField] private GameObject firstPersonWeapon;
    [SerializeField] private GameObject playerBody;

    [Header("Input")]
    [SerializeField] private KeyCode switchKey = KeyCode.V;

    [Header("Third Person Mouse Look")]
    [SerializeField] private float mouseSensitivity = 180f;
    [SerializeField] private float minPitch = -30f;
    [SerializeField] private float maxPitch = 60f;

    private bool isThirdPerson;
    private float yaw;
    private float pitch;

    private void Start()
    {
        if (thirdPersonViewPoint != null)
        {
            Vector3 currentRotation = thirdPersonViewPoint.eulerAngles;
            yaw = currentRotation.y;
            pitch = currentRotation.x;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SetFirstPerson();
    }

    private void Update()
    {
        if (Input.GetKeyDown(switchKey))
        {
            if (isThirdPerson)
            {
                SetFirstPerson();
            }
            else
            {
                SetThirdPerson();
            }
        }

        if (isThirdPerson)
        {
            HandleThirdPersonLook();
        }
    }

    private void HandleThirdPersonLook()
    {
        if (thirdPersonViewPoint == null)
        {
            return;
        }

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        yaw += mouseX * mouseSensitivity * Time.deltaTime;
        pitch -= mouseY * mouseSensitivity * Time.deltaTime;

        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        thirdPersonViewPoint.localRotation =
            Quaternion.Euler(pitch, yaw, 0f);
    }

    private void SetFirstPerson()
    {
        isThirdPerson = false;

        if (firstPersonCamera != null)
        {
            firstPersonCamera.SetActive(true);
        }

        if (thirdPersonCamera != null)
        {
            thirdPersonCamera.SetActive(false);
        }

        if (firstPersonWeapon != null)
        {
            firstPersonWeapon.SetActive(true);
        }

        if (playerBody != null)
        {
            playerBody.SetActive(true);
        }
    }

    private void SetThirdPerson()
    {
        isThirdPerson = true;

        if (firstPersonCamera != null)
        {
            firstPersonCamera.SetActive(false);
        }

        if (thirdPersonCamera != null)
        {
            thirdPersonCamera.SetActive(true);
        }

        if (firstPersonWeapon != null)
        {
            firstPersonWeapon.SetActive(false);
        }

        if (playerBody != null)
        {
            playerBody.SetActive(true);
        }

        if (thirdPersonViewPoint != null)
        {
            Vector3 currentRotation = thirdPersonViewPoint.eulerAngles;
            yaw = currentRotation.y;
            pitch = NormalizeAngle(currentRotation.x);
        }
    }

    private float NormalizeAngle(float angle)
    {
        if (angle > 180f)
        {
            angle -= 360f;
        }

        return angle;
    }
}