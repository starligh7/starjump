using UnityEngine;

public class Player : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    public float clampAngle = 80.0f;
    public float moveSpeed = 1.0f;

    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis

    private Rigidbody rigid;

    void Start()
    {
        cameraInit();
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
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

    private void move()
    {
        float yAngle = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;

        Debug.Log(yAngle / Mathf.PI);

        float xPos = Mathf.Sin(yAngle);
        float zPos = Mathf.Cos(yAngle);

        Vector3 velocity = new Vector3(xPos, 0, zPos);
        Debug.Log(velocity);

        transform.position += velocity * moveSpeed * Time.deltaTime;
    }
}