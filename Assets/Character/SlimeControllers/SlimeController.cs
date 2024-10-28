using System;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public float moveForce;
    public float jumpForce;
    public float maxMoveSpeed;
    public Rigidbody2D slimeRigidbody;
    public Collider2D slimeCollider;
    public Transform groundDetector;
    
    
    public bool IsJumping =>
        !Physics2D.Raycast(groundDetector.position, Vector2.down, 0.1f, LayerMask.GetMask("Platform"));
    
    private void Move(float moveInput)
    {
        slimeRigidbody.AddForce(Vector2.right * (moveInput * moveForce), ForceMode2D.Impulse);
        var velocity = slimeRigidbody.velocity;
        //Debug.Log($"velocity: {velocity}");
        if (velocity.x > maxMoveSpeed)
            slimeRigidbody.velocity = new Vector2(maxMoveSpeed, velocity.y);
        if (velocity.x < -maxMoveSpeed)
            slimeRigidbody.velocity = new Vector2(-maxMoveSpeed, velocity.y);
    }

    private void Jump()
    {
        slimeRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    private void Start()
    {
        slimeRigidbody = GetComponent<Rigidbody2D>();
        slimeCollider = GetComponent<Collider2D>();
        groundDetector = transform.Find("GroundDetector");
    }

    private void FixedUpdate()
    {
        // 키 안눌러도 moveInput 값은 바로 0이 되는게 아니고 서서히 줄어든다
        // 그래서 방향키에서 손 뗐는데 미끄러지듯이 멈추는 문제가 있어서 손에서 키 때면 moveInput을 0으로 함
        var isHorizontalPressed = Input.GetButton("Horizontal");
        var moveInput = isHorizontalPressed ? Input.GetAxis("Horizontal") : 0;
        Move(moveInput);

        var isJumpPressed = Input.GetButton("Jump");
        if (isJumpPressed && !IsJumping)
            Jump();
    }

    private void Update()
    {
    }
}