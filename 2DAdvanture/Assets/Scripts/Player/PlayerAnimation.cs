using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private PhysicsCheck pc;
    private PlayerController playerController;

    //获取组件
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PhysicsCheck>();
        playerController = GetComponent<PlayerController>();
    }

    //实时检测更新动画转变参数
    private void Update()
    {
        SetAnimation();
    }

    //将参数与Player物理参数联系
    public void SetAnimation()
    {
        anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetBool("isGround", pc.isGround);
        anim.SetBool("isCrouch", playerController.isCrouch);
    }
}