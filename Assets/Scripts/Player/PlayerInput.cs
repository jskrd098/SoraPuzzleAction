using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private InputSystem_Actions _inputAction;
    public Vector2 MoveInput { get; private set; }
    public bool JumpInput { get; private set; }

    private void Awake() => _inputAction = new InputSystem_Actions();
    public void OnEnable() => _inputAction.Enable();
    public void OnDisable() => _inputAction.Disable();

    public void ReadInput()
    {
        MoveInput = _inputAction.Player.Move.ReadValue<Vector2>();
        JumpInput = _inputAction.Player.Jump.WasPressedThisFrame();
    }
}
