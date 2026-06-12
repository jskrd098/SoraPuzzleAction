using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private InputSystem_Actions _inputAction;
    public Vector2Int MoveInput { get; private set; }
    public bool JumpInput { get; private set; }

    private void Awake() => _inputAction = new InputSystem_Actions();
    
    public void OnEnable() => _inputAction.Enable();
    public void OnDisable() => _inputAction.Disable();

    /// <summary>
    /// プレイヤーの入力を読み取るメソッド
    /// </summary>
    public void ReadInput()
    {
        MoveInput = new Vector2Int(
            Mathf.RoundToInt(_inputAction.Player.Move.ReadValue<Vector2>().x),
            Mathf.RoundToInt(_inputAction.Player.Move.ReadValue<Vector2>().y)
        );
        JumpInput = _inputAction.Player.Jump.WasPressedThisFrame();
    }
}
