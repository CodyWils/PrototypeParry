using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 12f;

    public Transform cameraTransform;

    private Vector2 movementInput;

    void Start()
    {
        
    }

    void Update()
    {
        movementInput = Vector2.zero;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed)
                movementInput.y = 1f;

            if (Keyboard.current.sKey.isPressed)
                movementInput.y = -1f;

            if (Keyboard.current.aKey.isPressed)
                movementInput.x = -1f;

            if (Keyboard.current.dKey.isPressed)
                movementInput.x = 1f;
        }

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 movement =
            (forward * movementInput.y + right * movementInput.x).normalized;

        transform.position += movement * speed * Time.deltaTime;
    }
}