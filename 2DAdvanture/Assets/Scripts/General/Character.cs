using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("基本属性")]
    public float maxHealth;
    public float currentHealth;

    [Header("受伤无敌")]
    public float invulnerableDuration;//无敌时间
    private float invulnerableCounter;//剩余无敌时间
    public bool invulnerable;


    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if(invulnerable)
        {
            invulnerableCounter -= Time.deltaTime;

            if(invulnerableCounter <= 0)
                invulnerable = false;
        }
    }

    //受伤方法
    public void TakeDamage(Attack attacker)
    {
        //如果处于无敌状态，就返回，不执行下面代码
        if (invulnerable)
            return;

        if (currentHealth - attacker.damage > 0)
        {
            currentHealth -= attacker.damage;
            TriggerInvulnerable();
        }
        else
        {
            currentHealth = 0;
            //触发死亡
        }
    }

    //无敌时间计时器
    private void TriggerInvulnerable()
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }
}
