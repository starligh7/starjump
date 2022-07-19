using UnityEngine;

public class Player : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    public float clampAngle = 80.0f;
    public float moveSpeed = 1.0f;
    public float jumpPower = 1.0f;
    public float gravity = 9.8f;

    public Vector3 velocity;

    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis

    private CharacterController controller;

    private void Start()
    {
        cameraInit();
        controller = GetComponent<CharacterController>();
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

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
    }

    private Vector3 moveDir = Vector3.zero;
    private void move()
    {

        if (controller.isGrounded)
        {
            moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            moveDir = Quaternion.Euler(0, transform.rotation.y, 0) * moveDir;

            moveDir = transform.TransformDirection(moveDir);
            moveDir *= moveSpeed;

            if (Input.GetButton("Jump"))
                moveDir.y = jumpPower;
        }

        moveDir.y -= Time.deltaTime * gravity;

        controller.Move(moveDir * Time.deltaTime);
    }
}