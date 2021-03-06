using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterStats : MonoBehaviour
{
    //更新血量UI
    //public event Action<int, int> UpdateHealthBarOnAttack;
    //模板数据
    public CharacterData_SO templateData;

    public CharacterData_SO characterData;
    public AttackData_SO attackData;//基础攻击力,角色则指近战攻击力
    //private RuntimeAnimatorController baseAnimator;
    //[HideInInspector]
    //public bool isCritical;//被暴击
    public bool isPlayer;

    void Awake()
    {
        if (templateData != null)
        {
            characterData = Instantiate(templateData);
        }
        //baseAnimator=GetComponent<Animator>().runtimeAnimatorController;
    }

    #region Read from Data_SO
    public int MaxHealth
    {
        get{if (characterData != null)return characterData.maxHealth;else return 0;}
        set{characterData.maxHealth = value;}
    }
    public int CurrentHealth
    {
        get { if (characterData != null) return characterData.currentHealth; else return 0; }
        set { characterData.currentHealth = value; }
    }
    // public int BaseDefence
    // {
    //     get { if (characterData != null) return characterData.baseDefence; else return 0; }
    //     set { characterData.baseDefence = value; }
    // }
    public int CurrentDefence
    {
        get { if (characterData != null) return characterData.currentDefence; else return 0; }
        set { characterData.currentDefence = value; }
    }
    public float CurrentPower
    {
        get { if (characterData != null) return characterData.currentPower; else return 0; }
        set { characterData.currentPower = value; }
    }
    #endregion

    #region Character Combat
    //受到攻击
    public void TakeDamage(CharacterStats attacker,CharacterStats defender)
    {
        int damage = Mathf.Max(attacker.CurrentDamage() - defender.CurrentDefence,0);
        defender.CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
        if(!isPlayer)
        {
            gameObject.GetComponent<EnemyController>().setTarget();
        }
        //暴击时触发挨打的人的受击动画
        // if (attacker.isCritical)
        // {
        //     defender.GetComponent<Animator>().SetTrigger("Hit");
        // }

        // //更新血量UI
        // UpdateHealthBarOnAttack?.Invoke(CurrentHealth, MaxHealth);
        // //结算经验值
        // if (defender.CurrentHealth <= 0)
        // {
        //     attacker.characterData.UpdateExp(defender.characterData.killPoint);
        // }
    }
    //重载take damage
    public void TakeDamage(int damage,CharacterStats defender)
    {
        int currentDamage = Mathf.Max(damage - defender.CurrentDefence, 0);
        defender.CurrentHealth = Mathf.Max(CurrentHealth - currentDamage, 0);
        if(!isPlayer)
        {
            gameObject.GetComponent<EnemyController>().setTarget();
        }
        // UpdateHealthBarOnAttack?.Invoke(CurrentHealth, MaxHealth);
        // if (defender.CurrentHealth <= 0)
        // {
        //     //GameManager.Instance.playerStats.characterData.UpdateExp(defender.characterData.killPoint);
        // }
    }

    private int CurrentDamage()
    {
        // float coreDamage = UnityEngine.Random.Range(attackData.minDamage, attackData.maxDamage);
        // if (isCritical)
        // {
        //     coreDamage *= attackData.criticalMultiplier;
        //     //Debug.Log("暴击!" + coreDamage);
        // }
        // return (int)coreDamage;
        return attackData.damage;
    }
    #endregion

    // #region Equip Weapon
    // public void ChangeWeapon(ItemData_SO weapon)
    // {
    //     UnEquipWeapon();
    //     EquipWeapon(weapon);
    // }
    // public void EquipWeapon(ItemData_SO weapon)
    // {
    //     if (weapon.weaponPrefab != null)
    //     {
    //         Instantiate(weapon.weaponPrefab, weaponSlot);
    //     }
    //     //更新攻击属性
    //     attackData.ApplyWeaponData(weapon.weaponData);
    //     //切换动画
    //     GetComponent<Animator>().runtimeAnimatorController=weapon.weaponAnimator;
    // }
    // public void UnEquipWeapon()
    // {
    //     if(weaponSlot.transform.childCount!=0)
    //     {
    //         for(int i=0;i<weaponSlot.transform.childCount;i++)
    //         {
    //             Destroy(weaponSlot.transform.GetChild(i).gameObject);
    //         }
    //     }
    //     attackData.ApplyWeaponData(baseAttackData);
    //     //切换动画
    //     GetComponent<Animator>().runtimeAnimatorController=baseAnimator;
    // }
    //#endregion
    #region Apply Data Change
    //回血
    public void ApplyHealth(int amount)
    {
        if(CurrentHealth+amount<=MaxHealth)
            CurrentHealth+=amount;
        else
            CurrentHealth=MaxHealth;
    }
    #endregion

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackData.attackRange);
    }
}
