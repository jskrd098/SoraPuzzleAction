using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private IWalkable _walkable;

    private void Awake()
    {
        // IWalkableを実装しているコンポーネントを取得
        _walkable = GetComponent<IWalkable>();
    }

    // InputSystemの"OnMove"メッセージを受け取る
    public void OnMove(InputValue value)
    {
        // 左右の入力値(-1.0f ~ 1.0f)を受け取る
        float moveInput = value.Get<Vector2>().x;

        // IWalkableのWalkメソッドを呼び出す
        _walkable?.Walk(moveInput);
    }
}
