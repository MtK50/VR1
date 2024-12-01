using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpHeight = 2f;
    public float gravity = -9.8f;
    public float mouseSensitivity = 2f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    public Transform cameraTransform;
    private float cameraPitch = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Lock the cursor for camera movement
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleView(); // Handle camera and player rotation
        MovePlayer(); // Handle movement and jumping
    }

    void MovePlayer()
    {
        // Check if player is grounded
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small constant to keep the player grounded
        }

        // Get input for movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Convert input to local space and apply rotation
        Vector3 move = (transform.right * moveX + transform.forward * moveZ).normalized;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleView()
    {
        // Mouse input for camera rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate the player horizontally
        Quaternion playerRotation = Quaternion.Euler(0f, transform.eulerAngles.y + mouseX, 0f);
        transform.rotation = playerRotation;

        // Rotate the camera vertically
        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f); // Limit vertical view
        cameraTransform.localRotation = Quaternion.Euler(0f, 0f, cameraPitch);
    }
}
