using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("基本属性")]
    public int damage;
    public float attackRange;
    public float attackRate;

    //这个other，代表的是受到伤害的一方
    //当检测到碰撞时，当前对象会寻找到对方对象，再进行生命值减少
    private void OnTriggerStay2D(Collider2D other)
    {
        //执行另一方的character脚本里的TakeDamage方法
        //this传入的是当前对象的整个Attack脚本
        other.GetComponent<Character>()?.TakeDamage(this);
    }
}
