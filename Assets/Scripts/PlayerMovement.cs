using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float runSpeed = 7f;
    [SerializeField] private float rotationSpeed = 10f;

    [Header("Jump Settings")]
    [SerializeField] private float jumpHeight = 1.8f;
    [SerializeField] private float gravity = -20f;

    [Header("Camera")]
    [SerializeField] private Transform cameraTransform;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private float animationSmoothTime = 0.1f;

    private CharacterController controller;
    private Vector3 verticalVelocity;

    private static readonly int SpeedHash =
        Animator.StringToHash("Speed");

    private static readonly int IsGroundedHash =
        Animator.StringToHash("IsGrounded");

    private static readonly int JumpHash =
        Animator.StringToHash("Jump");

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleJumpAndGravity();
        UpdateAnimator();
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (cameraTransform == null)
        {
            return;
        }

        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 moveDirection =
            (cameraForward * vertical + cameraRight * horizontal).normalized;

        bool isMoving = moveDirection.sqrMagnitude > 0.01f;
        bool isRunning =
            isMoving && Input.GetKey(KeyCode.LeftShift);

        float currentSpeed =
            isRunning ? runSpeed : walkSpeed;

        if (isMoving)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(moveDirection);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );

            controller.Move(
                moveDirection * currentSpeed * Time.deltaTime
            );
        }

        float animationSpeed = 0f;

        if (isMoving)
        {
            animationSpeed = isRunning ? 1f : 0.5f;
        }

        if (animator != null)
        {
            animator.SetFloat(
                SpeedHash,
                animationSpeed,
                animationSmoothTime,
                Time.deltaTime
            );
        }
    }

    private void HandleJumpAndGravity()
    {
        bool isGrounded = controller.isGrounded;

        if (isGrounded && verticalVelocity.y < 0f)
        {
            verticalVelocity.y = -2f;
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity.y =
                Mathf.Sqrt(jumpHeight * -2f * gravity);

            if (animator != null)
            {
                animator.SetTrigger(JumpHash);
            }
        }

        verticalVelocity.y += gravity * Time.deltaTime;

        controller.Move(verticalVelocity * Time.deltaTime);
    }

    private void UpdateAnimator()
    {
        if (animator == null)
        {
            return;
        }

        animator.SetBool(
            IsGroundedHash,
            controller.isGrounded
        );
    }
}