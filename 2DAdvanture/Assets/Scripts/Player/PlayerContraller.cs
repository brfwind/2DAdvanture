using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    public Rigidbody2D rb;
    public PhysicsCheck pc;
    public CapsuleCollider2D cc;
    public Vector2 inputDirection;
    [Header("基本参数")]
    public float speed;
    private float runSpeed;
    private float walkSpeed => speed / 2.7f; 
    public float jumpForce;
    public bool isCrouch;
    private Vector2 originalOffset;
    private Vector2 originalSize;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PhysicsCheck>();
        cc = GetComponent<CapsuleCollider2D>();

        originalOffset = cc.offset;
        originalSize = cc.size;

        inputControl = new PlayerInputControl();

        inputControl.Gameplay.Jump.started += Jump;

        #region 强制走路
        runSpeed = speed;
        inputControl.Gameplay.WalkButton.performed += ctx =>
        {
            if (pc.isGround)
                speed = walkSpeed;
        };

        inputControl.Gameplay.WalkButton.canceled += ctx =>
        {
            if (pc.isGround)
                speed = runSpeed;
        };
        #endregion

    }

    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void Update()
    {
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    //移动方法
    private void Move()
    {
        //Player的刚体组件中的速度，等于算出的速度（speed * 输入的x值 ， rb自己的y速度）
        //如果正在下蹲，让rb.velocity等于0，人物立刻停止移动，防止方向键和下蹲键同时按下时，人物蹲不下来
        if (isCrouch)
            rb.velocity = new Vector2(0f, 0f);
        else
            rb.velocity = new Vector2(speed * Time.deltaTime * inputDirection.x, rb.velocity.y);

        int faceDir = (int)transform.localScale.x;

        if (inputDirection.x > 0)
            faceDir = 1;
        if (inputDirection.x < 0)
            faceDir = -1;

        //根据运动方向，改变Player面部朝向
        transform.localScale = new Vector3(faceDir, 1, 1);

        if (inputDirection.y < -0.5f && pc.isGround)
        {
            isCrouch = true;
            cc.offset = new Vector2(-0.11f, 0.8f);
            cc.size = new Vector2(0.7f, 1.6f);
        }
        else
        {
            isCrouch = false;
            cc.offset = originalOffset;
            cc.size = originalSize;
        }
    }

    //跳跃方法（通过施加力）
    private void Jump(InputAction.CallbackContext obj)
    {
        if (pc.isGround)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
}