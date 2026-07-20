using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Camera Distance")]
    [SerializeField] private float distance = 7f;
    [SerializeField] private float minDistance = 3f;
    [SerializeField] private float maxDistance = 12f;
    [SerializeField] private float zoomSpeed = 3f;

    [Header("Camera Height")]
    [SerializeField] private float height = 2f;

    [Header("Mouse Rotation")]
    [SerializeField] private float mouseSensitivity = 150f;
    [SerializeField] private float minPitch = -20f;
    [SerializeField] private float maxPitch = 65f;

    [Header("Smoothing")]
    [SerializeField] private float positionSmoothSpeed = 12f;
    [SerializeField] private float rotationSmoothSpeed = 12f;

    [Header("Collision")]
    [SerializeField] private LayerMask collisionLayers = ~0;
    [SerializeField] private float collisionRadius = 0.25f;
    [SerializeField] private float collisionOffset = 0.2f;

    private float yaw;
    private float pitch = 20f;

    private void Start()
    {
        Vector3 currentAngles = transform.eulerAngles;
        yaw = currentAngles.y;
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        HandleMouseRotation();
        HandleZoom();
        UpdateCameraPosition();
        HandleCursor();
    }

    private void HandleMouseRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        yaw += mouseX * mouseSensitivity * Time.deltaTime;
        pitch -= mouseY * mouseSensitivity * Time.deltaTime;

        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
    }

    private void UpdateCameraPosition()
    {
        Vector3 targetPoint = target.position + Vector3.up * height;

        Quaternion desiredRotation = Quaternion.Euler(pitch, yaw, 0f);

        Vector3 desiredDirection =
            desiredRotation * Vector3.back;

        float finalDistance = distance;

        if (Physics.SphereCast(
            targetPoint,
            collisionRadius,
            desiredDirection,
            out RaycastHit hit,
            distance,
            collisionLayers,
            QueryTriggerInteraction.Ignore
        ))
        {
            finalDistance = Mathf.Max(
                minDistance,
                hit.distance - collisionOffset
            );
        }

        Vector3 desiredPosition =
            targetPoint + desiredDirection * finalDistance;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            positionSmoothSpeed * Time.deltaTime
        );

        Quaternion lookRotation =
            Quaternion.LookRotation(targetPoint - transform.position);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            lookRotation,
            rotationSmoothSpeed * Time.deltaTime
        );
    }

    private void HandleCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
           // Cursor.lockState = CursorLockMode.Locked;
           // Cursor.visible = false;
        }
    }
}