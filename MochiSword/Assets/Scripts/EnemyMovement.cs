using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Library.Scene;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] bool isXMove = true;       // 横移動するかどうか
    [SerializeField] bool isJump = true;        // ジャンプするかどうか
    [SerializeField] float xSpeed = 2.0f;       // 秒間速度 (block/sec)
    [SerializeField] GroundCheck groundCheck = default;   // 接地判定
    [SerializeField] LayerMask groundLayer = default;     // 崖際判定

    private float currentTime = 0f;     // ジャンプ用
    private float jumpTime = 3.0f;      // ジャンプの時間間隔 (sec)
    private float jumpPower = 4.0f;     // ジャンプの高さ
    private bool isGrounded;            // 接地判定に必要
    private Rigidbody2D rb;
    private Renderer rend;
    private Animator animator;
    private Vector2 vector;
    private Vector2 start, dir;


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
        start = (transform.position - transform.right * 0.5f * transform.localScale.x) - transform.right * 0.02f;
        dir = Vector2.down;
        //Debug.DrawRay(start, dir * 3.0f, Color.blue);    // デバッグ用
        if (IsInvert()) {
            Invert();
        }
        
        // 小ジャンプ判定
        if (isJump) {
            Jump();
        }

        isGrounded = groundCheck.isGrounded(); // 接地判定


        // 点滅
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
        xSpeed = -xSpeed;
    }

    private void Jump()
    {
        // 小ジャンプ処理
        currentTime += Time.deltaTime;
        if (currentTime > jumpTime) {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            currentTime = 0f;
        } else if (currentTime > jumpTime - 0.4f){
            animator.SetBool("isJump", true);
        } else if (currentTime > 0.4f && isGrounded) {
            animator.SetBool("isJump", false);
        }
    }

    private bool IsInvert()
    {
        // 崖際判定 (崖際ならTrue)
        return !Physics2D.Raycast(start, dir, 3.0f, groundLayer);
    }

    private IEnumerator DeathEffect()
    {
        // 点滅処理
        var wait = new WaitForSeconds(0.075f);
        for (int i = 0; i < 10; i++) {
            rend.enabled = !rend.enabled;
            yield return wait;
        }
        Destroy(this.gameObject);
    }
}