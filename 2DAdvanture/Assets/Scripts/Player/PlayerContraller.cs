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
    public Vector2 inputDirection;
    [Header("基本参数")]
    public float speed;
    private float runSpeed;
    private float walkSpeed => speed / 2.7f; 
    public float jumpForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PhysicsCheck>();

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
        rb.velocity = new Vector2(speed * Time.deltaTime * inputDirection.x, rb.velocity.y);

        int faceDir = (int)transform.localScale.x;

        if (inputDirection.x > 0)
            faceDir = 1;
        if (inputDirection.x < 0)
            faceDir = -1;

        //根据运动方向，改变Player面部朝向
        transform.localScale = new Vector3(faceDir, 1, 1);
    }

    //跳跃方法（通过施加力）
    private void Jump(InputAction.CallbackContext obj)
    {
        if (pc.isGround)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
}