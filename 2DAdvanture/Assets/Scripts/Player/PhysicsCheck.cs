using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("检测参数")]
    public Vector2 bottomOffset;
    public float checkRaduis;
    public LayerMask groundLayer;
    [Header("状态")]
    public bool isGround;
    
    //实时监测
    private void Update()
    {
        Check();
    }

    private void Check()
    {
        //检测Player下方的圆形区域有无带groundLayer标签的物体
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis, groundLayer);
    }

    //可视化圆形区域的范围
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRaduis);
    }
}