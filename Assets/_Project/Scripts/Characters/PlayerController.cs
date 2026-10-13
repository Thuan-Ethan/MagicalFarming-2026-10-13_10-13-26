using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float playerBaseSpeed = 5f;
    [SerializeField] float playerRunSpeed = 8f;

    private Vector2 moveInputVector;
    private Rigidbody2D rb;
    private InputAction moveAction;

    private float currentSpeed;


    void Awake()
    {
        rb = FindAnyObjectByType<Rigidbody2D>();
        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        moveInputVector = moveAction.ReadValue<Vector2>();
        moveInputVector = moveInputVector.normalized;

        // Pressed left/right ctrl to run 
        if (Keyboard.current != null && (Keyboard.current.leftCtrlKey.isPressed 
            || Keyboard.current.rightCtrlKey.isPressed))
        {
            rb.linearVelocity = moveInputVector * playerRunSpeed;
        }
        else
        {
            rb.linearVelocity = moveInputVector * playerBaseSpeed;
        }
    }
}
