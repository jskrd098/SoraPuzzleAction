using UnityEngine;

public class PlayerSensor : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _ladderLayer;
    [SerializeField] private Vector2 _groundCheckSize; // 接地判定用BoxColliderのサイズ
    [SerializeField] private Vector2 _groundCheckOffset; // 接地判定用BoxColliderの位置オフセット
    [SerializeField] private float _ladderCheckDistance; // 梯子判定用RayCast長さ(左右共通)
    [SerializeField] private Vector2 _ladderCheckOriginL; // 梯子判定用RayCast始点(左側)
    [SerializeField] private Vector2 _ladderCheckOriginR; // 梯子判定用RayCast始点(右側)
    [SerializeField] private BoxCollider2D _bodyCollider;
    [SerializeField] private float checkScale = 0.9f;
    private Vector2 target;
    private Vector2 checkSize;
    public bool _isGrounded { get; private set; }
    public bool _isOnLadder { get; private set; }
    public bool _isInLadderAnd { get; private set; }
    public bool _isInLadderOr { get; private set; }

    // コンストラクタ
    public PlayerSensor()
    {
        _isGrounded = false;
        _isOnLadder = false;
        _isInLadderAnd = false;
        _isInLadderOr = false;
    }

    // PlayerController の Fixed Update から毎フレーム呼び出される
    public void SensorUpdate()
    {
        _isGrounded = IsGrounded();
        _isOnLadder = IsOnLadder();
        _isInLadderAnd = IsInLadderAnd();
        _isInLadderOr = IsInLadderOr();
    }

    // 地面との接触判定
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

    // 梯子の中にいるかの判定
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

    // public bool CanMove(Vector2Int dir)
    // {
    //     // Playerの中心位置から判定先の位置を計算
    //     target = (Vector2)transform.position + dir;
    //     // PlayerのColliderのサイズを少し小さくしたサイズを判定用のサイズとする
    //     checkSize = _bodyCollider.bounds.size * checkScale;
    //     // 判定先の位置が移動可能かを判定
    //     return !Physics2D.OverlapBox(target, checkSize, 0f, _groundLayer);
    // }

#if UNITY_EDITOR
    // デバッグ用: シーンビューに判定範囲を表示(ヒエラルキー上で選択状態にすること)
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
        // // 移動可否判定の範囲を青色のワイヤーフレームで表示
        // Gizmos.color = Color.blue;
        // if (_bodyCollider == null) return;
        // Vector2 target = (Vector2)transform.position + Vector2.right * _bodyCollider.bounds.size.x;
        // Vector2 checkSize = _bodyCollider.bounds.size * checkScale;
        // Gizmos.DrawWireCube(target, checkSize);
    }
#endif
}