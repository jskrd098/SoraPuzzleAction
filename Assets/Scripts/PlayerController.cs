using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rBody;
    float axisH = 0.0f; // Input
    public float moveSpd;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // ¶‰EˆÚ“®
        axisH = Input.GetAxisRaw("Horizontal");
        if      (axisH > 0.0f) transform.localScale = new Vector2( 1, 1);
        else if (axisH < 0.0f) transform.localScale = new Vector2(-1, 1);
    }

    void FixedUpdate()
    {
        rBody.linearVelocity = new Vector2(moveSpd * axisH, rBody.linearVelocity.y);
    }
}
