using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] bool isXMove = true;       // 横移動するかどうか
    [SerializeField] bool isInvert = true;      // 反転するかどうか
    [SerializeField] float xSpeed = 2.0f;       // 秒間速度 (block/sec)
    [SerializeField] float invertTime = 2.0f;   // 往復時間 (sec)
    [SerializeField] bool isJump = true;        // ジャンプするかどうか
    [SerializeField] GroundCheck groundCheck;   // 接地判定用

    private float currentTime = 0f;     // 横移動用
    private float currentTime2 = 0f;    // ジャンプ用
    private float jumpTime = 0.5f;      // ジャンプの時間間隔 (sec)
    private float jumpPower = 3.0f;     // ジャンプの高さ
    private Rigidbody2D rb;
    private Vector2 vector;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // 反転判定
        if (isInvert) {
            Invert();
        }

        // 小ジャンプ判定
        if (groundCheck.isGrounded() && isJump) {
            Jump();
        }
    }
    private void FixedUpdate()
    {
        if (!isXMove) {
            return;
        }

        // 移動処理
        vector.x = -xSpeed;
        vector.y = rb.velocity.y;
        rb.velocity = vector;
    }

    private void Invert()
    {
        // 反転処理
        currentTime += Time.deltaTime;
        if (currentTime > invertTime) {
            xSpeed = -xSpeed;
            currentTime = 0f;
        }
    }

    private void Jump()
    {
        // 小ジャンプ処理
        currentTime2 += Time.deltaTime;
        if (currentTime2 > jumpTime) {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            currentTime2 = 0f;
        }
    }
}