using UnityEngine;

public class Movimento : MonoBehaviour
{
    public float movementSpeed = 10f;
    public float boostMultiplier = 2f;
    public float mouseSensitivity = 3f;
    public bool requireRightMouse = true;

    private float rotationX;
    private float rotationY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        bool isRotating = !requireRightMouse || Input.GetMouseButton(1);

        // Mouse look
        if (isRotating)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            rotationX += Input.GetAxis("Mouse X") * mouseSensitivity;
            rotationY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            rotationY = Mathf.Clamp(rotationY, -90f, 90f);

            transform.rotation = Quaternion.Euler(rotationY, rotationX, 0f);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Movement
        Vector3 direction = new Vector3(
            Input.GetAxis("Horizontal"),
            (Input.GetKey(KeyCode.E) ? 1 : 0) - (Input.GetKey(KeyCode.Q) ? 1 : 0),
            Input.GetAxis("Vertical")
        );

        float speed = movementSpeed * (Input.GetKey(KeyCode.LeftShift) ? boostMultiplier : 1f);
        transform.Translate(direction * speed * Time.deltaTime, Space.Self);
    }
}