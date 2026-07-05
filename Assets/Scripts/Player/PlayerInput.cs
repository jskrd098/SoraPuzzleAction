using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    private InputSystem_Actions _inputAction;
    public Vector2Int moveInput { get; private set; }
    public bool jumpInput { get; private set; }

    private void Awake() => _inputAction = new InputSystem_Actions();
    public void OnEnable() => _inputAction.Enable();
    public void OnDisable() => _inputAction.Disable();

    /// <summary>
    /// プレイヤーの入力を読み取るメソッド
    /// </summary>
    public void ReadInput()
    {
        moveInput = new Vector2Int(
            Mathf.RoundToInt(_inputAction.Player.Move.ReadValue<Vector2>().x),
            Mathf.RoundToInt(_inputAction.Player.Move.ReadValue<Vector2>().y)
        );
        jumpInput = _inputAction.Player.Jump.WasPressedThisFrame();
    }
}
