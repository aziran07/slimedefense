using System;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    private float _moveInput;
    private bool _jumpInput;
    private bool _isJumping;
    public float moveForce;
    public float jumpForce;
    public float maxMoveSpeed;
    public Rigidbody2D slimeRigidbody;
    public Collider2D slimeCollider;
    private void Move()
    {
        slimeRigidbody.AddForce(Vector2.right * (_moveInput * moveForce), ForceMode2D.Force);
        if (slimeRigidbody.velocity.x > maxMoveSpeed)
            slimeRigidbody.velocity = new Vector2(maxMoveSpeed, slimeRigidbody.velocity.y);
        else if (slimeRigidbody.velocity.x < -maxMoveSpeed)
            slimeRigidbody.velocity = new Vector2(-maxMoveSpeed, slimeRigidbody.velocity.y);
    }

    private void Jump()
    {
        slimeRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    private void Start()
    {
        slimeRigidbody = GetComponent<Rigidbody2D>();
        slimeCollider = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    
    {
        Move();
        _isJumping = !Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Platform"));
        if (_jumpInput && !_isJumping) Jump();
    }

    private void Update()
    {
        _moveInput = Input.GetAxis("Horizontal");
        _jumpInput = Input.GetButton("Jump");
    }
}