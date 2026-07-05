using UnityEngine;

public class PlayerSensor : MonoBehaviour, ICharacterSensor
{
    private PlayerController _player;
    private MovementDirectionResolver _movementDirectionResolver;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _ladderLayer;
    [SerializeField] private Vector2 _groundCheckSize; // 接地判定用BoxColliderのサイズ
    [SerializeField] private Vector2 _groundCheckOffset; // 接地判定用BoxColliderの位置オフセット
    [SerializeField] private float _ladderCheckDistance; // 梯子判定用RayCast長さ(左右共通)
    [SerializeField] private Vector2 _ladderCheckOriginL; // 梯子判定用RayCast始点(左側)
    [SerializeField] private Vector2 _ladderCheckOriginR; // 梯子判定用RayCast始点(右側)
    [SerializeField] private BoxCollider2D _bodyCollider;
    [SerializeField] private float checkScale = 0.9f;
    // public bool _isGrounded { get; private set; }
    public bool _isOnLadder { get; private set; }
    public bool _isInLadderAnd { get; private set; }
    public bool _isInLadderOr { get; private set; }

    private void Awake()
    {
        _player = GetComponent<PlayerController>();
        _movementDirectionResolver = new MovementDirectionResolver();
        // _isGrounded = false;
        _isOnLadder = false;
        _isInLadderAnd = false;
        _isInLadderOr = false;
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
            _movementDirectionResolver.ResolveDirection(_player, _player._playerInput.moveInput);
        }

        // _isGrounded = IsGrounded();
        IsGrounded();
        _isOnLadder = IsOnLadder();
        _isInLadderAnd = IsInLadderAnd();
        _isInLadderOr = IsInLadderOr();
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

        if (_isInLadderOr)
        {
            return false; // 梯子の頂上にいる場合のみ OnLadder を True とするため、IsInLadder が True だと OnLadder は False になる
        }
        else
        {
            // Playerの中心位置から指定したオフセット分ずらした位置を中心にする
            Vector2 checkPosition = position + _groundCheckOffset;
            // 四角形の範囲内に梯子があるかを判定
            return Physics2D.OverlapBox(checkPosition, _groundCheckSize, 0f, _ladderLayer);
        }
    }

    public bool IsInLadder()
    {
        return _isInLadderAnd || _isInLadderOr;
    }

    public bool CanMove(Vector2 direction)
    {
        var collider = _bodyCollider ?? GetComponent<BoxCollider2D>();
        if (collider == null)
        {
            return true;
        }

        Vector2 targetPosition = (Vector2)transform.position + direction.normalized * 1f;
        Vector2 size = collider.size * checkScale;
        Collider2D hit = Physics2D.OverlapBox(targetPosition, size, 0f, _groundLayer);
        return hit == null;
    }

    /// <summary>
    /// 梯子の中にいるかの判定
    /// </summary>
    /// <returns>
    /// 梯子の中にいる場合:true、そうでない場合:false
    /// </returns>
    private bool IsInLadderAnd() { return IsInLadderL() && IsInLadderR(); }
    private bool IsInLadderOr() { return IsInLadderL() || IsInLadderR(); }
    private bool IsInLadderL()
    {
        // Playerの中心位置
        Vector2 position = (Vector2)transform.position;
        // RayCastの始点をPlayerの中心位置から指定したオフセット分ずらした位置にする
        Vector2 origin = position + _ladderCheckOriginL;
        // RayCastを上方向に指定した距離だけ飛ばして、梯子レイヤーに当たるかを判定
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.up, _ladderCheckDistance, _ladderLayer);
        // RayCastが梯子レイヤーに当たったかどうかを返す
        return hit.collider != null;
    }
    private bool IsInLadderR()
    {
        // Playerの中心位置
        Vector2 position = (Vector2)transform.position;
        // RayCastの始点をPlayerの中心位置から指定したオフセット分ずらした位置にする
        Vector2 origin = position + _ladderCheckOriginR;
        // RayCastを上方向に指定した距離だけ飛ばして、梯子レイヤーに当たるかを判定
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.up, _ladderCheckDistance, _ladderLayer);
        // RayCastが梯子レイヤーに当たったかどうかを返す
        return hit.collider != null;
    }

#if UNITY_EDITOR
    /// <summary>
    /// デバッグ用: シーンビューに判定範囲を表示する(注：ヒエラルキー上で選択状態にすること) 
    /// </summary>
    private void OnDrawGizmos()
    {
        // 接地判定の範囲を緑色のワイヤーフレームで表示
        Gizmos.color = Color.green;
        Vector2 checkPosition = (Vector2)transform.position + _groundCheckOffset;
        Gizmos.DrawWireCube(checkPosition, _groundCheckSize);
        // 梯子判定のRayCastを赤色の線で表示
        Gizmos.color = Color.red;
        Vector2 originL = (Vector2)transform.position + _ladderCheckOriginL;
        Vector2 originR = (Vector2)transform.position + _ladderCheckOriginR;
        Gizmos.DrawLine(originL, originL + Vector2.up * _ladderCheckDistance);
        Gizmos.DrawLine(originR, originR + Vector2.up * _ladderCheckDistance);
    }
#endif
}