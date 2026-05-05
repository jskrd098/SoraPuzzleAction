using UnityEngine;

public class PlayerCensor : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _ladderLayer;

    // 足元の判定サイズ(Inspectorからキャラの横幅に合わせて調整)
    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.6f, 0.2f);

    // 足元のズレ(中心からどれだけ下に判定を置くか)
    [SerializeField] private Vector2 _groundCheckOffset = new Vector2(0f, -0.5f);

    public bool _isGrounded { get; private set; }
    public bool _isOnLadder { get; private set; }
    public bool _isInLadder { get; private set; }

    public void Enter()
    {
        _isGrounded = false;
        _isOnLadder = false;
        _isInLadder = false;
    }

    public void CensorUpdate(Collider2D _collider2D)
    {
        _isGrounded = IsGrounded(_collider2D.transform.position);
        _isOnLadder = IsOnLadder(_collider2D);
        _isInLadder = IsInLadder(_collider2D);

        Debug.Log("Grounded: " + _isGrounded);
        Debug.Log("OnLadder: " + _isOnLadder);
        Debug.Log("InLadder: " + _isInLadder);
    }

    // 地面との接触判定
    public bool IsGrounded(Vector2 position)
    {
        // Playerの中心位置から指定したオフセット分ずらした位置を中心にする
        Vector2 checkPosition = position + _groundCheckOffset;

        // 四角形の範囲内に地面があるかを判定
        return Physics2D.OverlapBox(checkPosition, _groundCheckSize, 0f, _groundLayer);
    }

    public bool IsOnLadder(Collider2D bodyCollider)
    {
        if (IsInLadder(bodyCollider))
        {
            return false; // 梯子に重なっているが、地面には接触していない場合は梯子にいると判定
        }
        else
        {
            // Playerの中心位置から指定したオフセット分ずらした位置を中心にする
            Vector2 checkPosition = (Vector2)transform.position + _groundCheckOffset;

            // 四角形の範囲内に梯子があるかを判定
            return Physics2D.OverlapBox(checkPosition, _groundCheckSize, 0f, _ladderLayer);
        }
    }

    public bool IsInLadder(Collider2D bodyCollider)
    {
        return bodyCollider.IsTouchingLayers(_ladderLayer); // 梯子と重なっているかの判定
    }

    // デバッグ用: シーンビューに判定範囲を表示(ヒエラルキー上で選択状態にすること)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector2 checkPosition = (Vector2)transform.position + _groundCheckOffset;
        Gizmos.DrawWireCube(checkPosition, _groundCheckSize);
    }
}