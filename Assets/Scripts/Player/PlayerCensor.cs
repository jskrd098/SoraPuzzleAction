using UnityEngine;

public class PlayerCensor : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _ladderLayer;
    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.6f, 0.2f); // 接地判定用BoxColliderのサイズ
    [SerializeField] private Vector2 _groundCheckOffset = new Vector2(0f, -0.5f); // 接地判定用BoxColliderの位置オフセット
    [SerializeField] private float _ladderCheckDistance = 0.3f; // 梯子判定用RayCast長さ(左右共通)
    [SerializeField] private Vector2 _ladderCheckOriginL = new Vector2(-0.25f, 0.3f); // 梯子判定用RayCast始点(左側)
    [SerializeField] private Vector2 _ladderCheckOriginR = new Vector2(0.25f, 0.3f); // 梯子判定用RayCast始点(右側)

    public bool _isGrounded { get; private set; }
    public bool _isOnLadder { get; private set; }
    public bool _isInLadder { get; private set; }
    public bool _isInLadderL { get; private set; }
    public bool _isInLadderR { get; private set; }

    // コンストラクタ
    public PlayerCensor()
    {
        _isGrounded = false;
        _isOnLadder = false;
        _isInLadder = false;
        _isInLadderL = false;
        _isInLadderR = false;
    }

    // PlayerController の Fixed Update から毎フレーム毎に呼び出される
    public void CensorUpdate(Collider2D _collider2D)
    {
        _isGrounded = IsGrounded(_collider2D.transform.position);
        _isOnLadder = IsOnLadder(_collider2D);
        _isInLadder = IsInLadder(_collider2D);

        if (_isGrounded) Debug.Log("Grounded");
        if (_isOnLadder) Debug.Log("OnLadder");
        if (_isInLadder) Debug.Log("InLadder");
    }

    // 地面との接触判定
    public bool IsGrounded(Vector2 position)
    {
        // Playerの中心位置から指定したオフセット分ずらした位置を中心にする
        Vector2 checkPosition = position + _groundCheckOffset;

        // 四角形の範囲内に地面があるかを判定
        return Physics2D.OverlapBox(checkPosition, _groundCheckSize, 0f, _groundLayer);
    }

    // 梯子の頂上にいるかの判定
    public bool IsOnLadder(Collider2D bodyCollider)
    {
        if (IsInLadder(bodyCollider))
        {
            return false; // 梯子の頂上にいる場合のみ OnLadder を True とするため、IsInLadder が True だと OnLadder は False になる
        }
        else
        {
            // Playerの中心位置から指定したオフセット分ずらした位置を中心にする
            Vector2 checkPosition = (Vector2)transform.position + _groundCheckOffset;

            // 四角形の範囲内に梯子があるかを判定
            return Physics2D.OverlapBox(checkPosition, _groundCheckSize, 0f, _ladderLayer);
        }
    }

    // 梯子の中にいるかの判定
    // Raycastを使用して、Playerの左右から指定した距離内に梯子があるかを判定
    public bool IsInLadderL()
    {
        Vector2 origin = (Vector2)transform.position + _ladderCheckOriginL;

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.up, _ladderCheckDistance, _ladderLayer);

        return hit.collider != null;
    }
    public bool IsInLadderR()
    {
        Vector2 origin = (Vector2)transform.position + _ladderCheckOriginR;

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.up, _ladderCheckDistance, _ladderLayer);

        return hit.collider != null;
    }

    public bool IsInLadder(Collider2D bodyCollider)
    {
        return bodyCollider.IsTouchingLayers(_ladderLayer); // 梯子と重なっているか
    }

    // デバッグ用: シーンビューに判定範囲を表示(ヒエラルキー上で選択状態にすること)
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector2 checkPosition = (Vector2)transform.position + _groundCheckOffset;
        Gizmos.DrawWireCube(checkPosition, _groundCheckSize);

        Gizmos.color = Color.red;
        Vector2 originL = (Vector2)transform.position + _ladderCheckOriginL;
        Vector2 originR = (Vector2)transform.position + _ladderCheckOriginR;
        Gizmos.DrawLine(originL, originL + Vector2.up * _ladderCheckDistance);
        Gizmos.DrawLine(originR, originR + Vector2.up * _ladderCheckDistance);
    }
}