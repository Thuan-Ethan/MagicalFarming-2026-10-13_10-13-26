using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float playerBaseSpeed = 5f;
    [SerializeField] float playerRunSpeed = 8f;

    [SerializeField] Animator animator;

    private Vector2 moveInputVector;
    private Rigidbody2D rb;
    private InputAction moveAction;

    private float currentSpeed;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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

        AnimateMovement(moveInputVector);
    }

    private void FixedUpdate()
    {
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

    void AnimateMovement(Vector2 direction)
    {
        if(direction.magnitude > 0)
        {
            animator.SetBool("isMoving", true);

            animator.SetFloat("horizontal", direction.x);
            animator.SetFloat("vertical", direction.y);
        } else
        {
            animator.SetBool("isMoving", false);
        }
    }

}
