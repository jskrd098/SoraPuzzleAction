using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float jumpPower = 5f;
    private Rigidbody2D rb;
    private InputAction walkAction;
    private InputAction jumpAction;

    void Awake()
    {
        walkAction = InputSystem.actions.FindAction("Walk");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    void OnEnable()
    {
        walkAction.performed += OnWalk;
        walkAction.canceled += OnWalkCanceled;
        jumpAction.started += OnJump;
    }

    void OnDisable()
    {
        walkAction.performed -= OnWalk;
        walkAction.canceled -= OnWalkCanceled;
        jumpAction.started -= OnJump;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
    }


    void FixedUpdate()
    {

    }

    void OnWalk(InputAction.CallbackContext context)
    {
        Vector2 walk = context.ReadValue<Vector2>();
        rb.linearVelocityX = walk.x * walkSpeed;
        Debug.Log("OnWalk");
    }

    void OnWalkCanceled(InputAction.CallbackContext context)
    {
        rb.linearVelocityX = 0f;
        Debug.Log("OnWalkCanceled");
    }

    void OnJump(InputAction.CallbackContext context)
    {
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        Debug.Log("OnJump");
    }
}