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
    [SerializeField] GroundCheck groundCheck = default;   // 接地判定

    private float currentTime = 0f;     // 横移動用
    private float currentTime2 = 0f;    // ジャンプ用
    private float jumpTime = 3.0f;      // ジャンプの時間間隔 (sec)
    private float jumpPower = 4.0f;     // ジャンプの高さ
    private bool isGrounded;            // 接地判定に必要
    private Rigidbody2D rb;
    private Renderer rend;
    private Animator animator;
    private Vector2 vector;

    private void Start()
    {
        rb       = GetComponent<Rigidbody2D>();
        rend     = GetComponent<Renderer>();
        animator = GetComponent<Animator>();
        var wait = new WaitForSeconds(0.2f);
    }

    private void Update()
    {
        // 反転判定
        if (isInvert) {
            Invert();
        }

        // 小ジャンプ判定
        if (isJump) {
            Jump();
        }

        isGrounded = groundCheck.isGrounded(); // 接地判定

        if (Input.GetKeyDown(KeyCode.P)) {
            StartCoroutine("DeathEffect");
        }
    }
    private void FixedUpdate()
    {
        // 敵の向き調整
        if (xSpeed > 0f) {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        } else if (xSpeed < 0f) {
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }

        // 移動処理
        if (isXMove) {
            vector.x = -xSpeed;
        }
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
        } else if (currentTime2 > jumpTime - 0.4f){
            animator.SetBool("isJump", true);
        } else if (currentTime2 > 0.4f && isGrounded) {
            animator.SetBool("isJump", false);
        }
    }

    private IEnumerator DeathEffect()
    {
        // 点滅処理
        var wait = new WaitForSeconds(0.075f);
        for (int i = 0; i < 10; i++) {
            rend.enabled = !rend.enabled;
            yield return wait;
        }
    }
}