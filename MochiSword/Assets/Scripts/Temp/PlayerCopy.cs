using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCopy : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer = default;
    [SerializeField] CloudManager cloudManager = default;

    private float xSpeed;
    private float xRate;
    public float jumpPower;
    private Rigidbody2D rb;
    private Vector2 vector;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        xRate = 7.0f;
    }

    private void Update()
    {
        xSpeed = Input.GetAxisRaw("Horizontal") * xRate;

        if (Input.GetKeyDown("space") && HitGround()) {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (xSpeed > 0f) {
            transform.localScale = new Vector2(1f, 1f);
        } else if (xSpeed < 0f) {
            transform.localScale = new Vector2(-1f, 1f);
        }

        vector.x = xSpeed;
        vector.y = rb.velocity.y;

        rb.velocity = vector;
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    private bool HitGround()
    {
        return Physics2D.Linecast(transform.position - transform.right * 0.2f - transform.up * 0.48f, transform.position - transform.up * 0.53f, groundLayer)
             || Physics2D.Linecast(transform.position + transform.right * 0.2f - transform.up * 0.48f, transform.position - transform.up * 0.53f, groundLayer);
    }
}
