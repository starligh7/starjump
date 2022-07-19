using UnityEngine;

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
    }

    private void Update()
    {
        look();
        move();
    }

    private void cameraInit()
    {
        Vector3 rot = transform.localRotation.eulerAngles;

        rotY = rot.y;
        rotX = rot.x;

        Cursor.visible = false;
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
    }

    private Vector3 moveDir = Vector3.zero;
    private void move()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        anim.SetFloat("x", x);
        anim.SetFloat("y", z);


        Vector3 move = transform.TransformDirection(x, 0, z);
        move.y = 0;

        anim.SetBool("isWalk", !(x == 0 && z == 0));

        move.Normalize();

        controller.Move(move * moveSpeed * Time.deltaTime);

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
}