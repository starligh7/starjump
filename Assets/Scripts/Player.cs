using UnityEngine;
using TMPro.Examples;

public class Player : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    public float clampAngle = 80.0f;
    public float moveSpeed = 1.0f;
    public float jumpPower = 1.0f;
    public float gravity = 9.8f;

    public bool isGrounded;

    public Vector3 velocity;

    public GameObject head;

    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis

    private CharacterController controller;
    private Animator anim;

    private void Start()
    {
        cameraInit();
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        mainCamera = Camera.main.GetComponent<CameraController>();
    }

    private void Update()
    {
        look();
        move();

        if (Input.GetKeyDown(KeyCode.F5))
        {
            toggleCamera();
        }

        if (transform.position.y <= 0)
        {
            velocity = Vector3.zero;
            transform.position = Vector3.zero;
        }
    }

    private void cameraInit()
    {
        Vector3 rot = transform.localRotation.eulerAngles;

        rotY = rot.y;
        rotX = rot.x;

    }

    private void look()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");


        rotY += mouseX * mouseSensitivity * Time.deltaTime;
        rotX += mouseY * mouseSensitivity * Time.deltaTime;


        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Debug.Log(new Vector2(rotX, rotY));

        Quaternion localRotation = Quaternion.Euler(0, rotY, 0);
        transform.rotation = localRotation;

        Vector3 headRot = head.transform.localRotation.eulerAngles;
        headRot.x = rotX;

        head.transform.localRotation = Quaternion.Euler(headRot);

        //cursor lock

        if (EscScript.GameIsPaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private Vector3 moveDir = Vector3.zero;
    private void move()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        anim.SetFloat("x", x);
        anim.SetFloat("y", z);


        Vector3 move = transform.TransformDirection(x, 0, z);
        move.y = 0;

        anim.SetBool("isWalk", !(x == 0 && z == 0));

        move.Normalize();

        CollisionFlags flag = controller.Move(move * moveSpeed * Time.deltaTime);
        Debug.Log(flag);

        if ((CollisionFlags.CollidedAbove & flag) != 0)
        {
            velocity.y = 0;
        }

        if (isGrounded)
        {
            // Changes the height position of the player..
            if (Input.GetButton("Jump"))
            {
                velocity.y += Mathf.Sqrt(jumpPower * 3.0f * gravity);
            }
        }

        velocity.x *= 0.5f;
        velocity.z *= 0.5f;

        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private bool isThridPerson = false;
    private CameraController mainCamera;

    private void toggleCamera()
    {
        isThridPerson = !isThridPerson;
        Camera.main.cullingMask ^= 1 << 3;

        if (isThridPerson)
        {
            mainCamera.enabled = true;
            mainCamera.gameObject.transform.SetParent(null);
        }
        else
        {
            mainCamera.enabled = false;
            mainCamera.gameObject.transform.SetParent(head.transform);
            mainCamera.gameObject.transform.localPosition = new Vector3(0, 0.01399f, -0.00156f);
            mainCamera.gameObject.transform.localRotation = Quaternion.identity;
        }
    }
}