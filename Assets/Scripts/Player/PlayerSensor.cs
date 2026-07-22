using UnityEngine;

public class PlayerSensor : MonoBehaviour, ICharacterSensor
{
    private PlayerController _player;
    private MovementDirectionResolver _movementDirectionResolver;
    [SerializeField] private LayerMask _groundLayer; // 接地判定用LayerMask
    [SerializeField] private Vector2 _groundCheckSize; // 接地判定用BoxColliderのサイズ
    [SerializeField] private Vector2 _groundCheckOffset; // 接地判定用BoxColliderの位置オフセット
    [SerializeField] private LayerMask _ladderLayer; // 梯子判定用LayerMask
    [SerializeField] private Vector2 _ladderCheckSize; // 梯子判定用BoxColliderのサイズ
    [SerializeField] private Vector2 _ladderCheckOffset; // 梯子判定用BoxColliderの位置オフセット
    [SerializeField] private BoxCollider2D _bodyCollider; // PlayerのBoxCollider2Dコンポーネント
    [SerializeField] private float checkScale = 0.9f; // 移動可能判定用BoxColliderのサイズ調整用スケール

    private void Awake()
    {
        _player = GetComponent<PlayerController>();
        _movementDirectionResolver = new MovementDirectionResolver();
    }

    /// <summary>
    /// センサーの更新を行う
    /// </summary>
    public void SensorUpdate()
    {
        if (_player == null)
        {
            _player = GetComponent<PlayerController>();
        }

        if (_player != null && _player._playerInput != null)
        {
            Vector2Int resolvedInput = _movementDirectionResolver.ResolveDirection(_player, _player._playerInput.moveInput);
            _player._playerInput.SetMoveInput(resolvedInput);
        }
    }

    /// <summary>
    /// 地面との接触判定を行う
    /// </summary>
    /// <returns>
    /// 接地している場合:true、そうでない場合:false
    /// </returns>
    public bool IsGrounded()
    {
        // Playerの中心位置
        Vector2 position = (Vector2)transform.position;
        // Playerの中心位置から指定したオフセット分ずらした位置を中心にする
        Vector2 checkPosition = position + _groundCheckOffset;
        // 四角形の範囲内に地面があるかを判定
        return Physics2D.OverlapBox(checkPosition, _groundCheckSize, 0f, _groundLayer);
    }

    /// <summary>
    /// 梯子の頂上にいるかの判定 
    /// </summary>
    /// <returns>
    /// 梯子の頂上にいる場合:true、そうでない場合:false
    /// </returns>
    public bool IsOnLadder()
    {
        // Playerの中心位置
        Vector2 position = (Vector2)transform.position;
        // IsInLadderがTrueの場合、OnLadderはFalseとする
        if (IsInLadder()) return false;
        // Playerの中心位置から指定したオフセット分ずらした位置を中心にする
        Vector2 checkPosition = position + _groundCheckOffset;
        // 四角形の範囲内に梯子があるかを判定
        return Physics2D.OverlapBox(checkPosition, _groundCheckSize, 0f, _ladderLayer);
    }

    /// <summary>
    /// 梯子の中にいるかの判定
    /// </summary>
    /// <returns>
    /// 梯子の中にいる場合:true、そうでない場合:false
    /// </returns>
    public bool IsInLadder()
    {
        // Playerの中心位置
        Vector2 position = (Vector2)transform.position;
        // Playerの中心位置から指定したオフセット分ずらした位置を中心にする
        Vector2 checkPosition = position + _ladderCheckOffset;
        // 四角形の範囲内に梯子があるかを判定
        return Physics2D.OverlapBox(checkPosition, _ladderCheckSize, 0f, _ladderLayer);
    }

    /// <summary>
    /// 指定した方向に移動可能かの判定
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public bool CanMove(Vector2 direction)
    {
        BoxCollider2D collider = _bodyCollider ?? GetComponent<BoxCollider2D>();
        if (collider == null)
        {
            return true;
        }

        Vector2 targetPosition = (Vector2)transform.position + direction.normalized * 1f;
        Vector2 size = collider.size * checkScale;
        Collider2D hit = Physics2D.OverlapBox(targetPosition, size, 0f, _groundLayer);
        return hit == null;
    }

#if UNITY_EDITOR
    /// <summary>
    /// デバッグ用: シーンビューに判定範囲を表示する(注：ヒエラルキー上で選択状態にすること) 
    /// </summary>
    private void OnDrawGizmos()
    {
        // 接地判定の範囲を緑色のワイヤーフレームで表示
        Gizmos.color = Color.green;
        Vector2 groundCheckPosition = (Vector2)transform.position + _groundCheckOffset;
        Gizmos.DrawWireCube(groundCheckPosition, _groundCheckSize);
        // 梯子判定の範囲を青色のワイヤーフレームで表示
        Gizmos.color = Color.blue;
        Vector2 ladderCheckPosition = (Vector2)transform.position + _ladderCheckOffset;
        Gizmos.DrawWireCube(ladderCheckPosition, _ladderCheckSize);
    }
#endif
}