using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Attack",menuName = "Attack/Attack Data")]
public class AttackData_SO : ScriptableObject
{
    //攻击距离
    public float attackRange;
    //技能距离
    public float skillRange;
    //CD
    public float coolDown;
    //攻击力
    public int damage;
    // //最小攻击
    // public int minDamage;
    // //最大攻击
    // public int maxDamage;
    // //爆伤
    // public float criticalMultiplier;
    // //暴击率
    // public float criticalChance;

    // public void ApplyWeaponData(AttackData_SO weapon)
    // {
    //     attackRange = weapon.attackRange;
    //     skillRange = weapon.skillRange;
    //     coolDown = weapon.coolDown;
    //     minDamage = weapon.minDamage;
    //     maxDamage = weapon.maxDamage;
    //     criticalMultiplier = weapon.criticalMultiplier;
    //     criticalChance = weapon.criticalChance;
    // }
}
