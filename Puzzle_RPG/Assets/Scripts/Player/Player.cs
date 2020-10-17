using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public PlayerData(float _playerHp,float _fireDamage,float _waterDamage, float _plantDamage, float _fireDefense , float _waterDefense, float _plantDefense)
    {
        playerHp = _playerHp; fireDamage = _fireDamage; waterDamage = _waterDamage; plantDamage = _plantDamage; fireDefense = _fireDefense; waterDefense = _waterDefense; plantDefense = _plantDefense;
    }
    public float playerHp, fireDamage, waterDamage, plantDamage, fireDefense, waterDefense, plantDefense;
}
public class Player : MonoBehaviour
{
    PlayerData playerData;
    Animator anim;
    public PlayerData PlayerData
    {
        get { return playerData; }
        set { playerData = value; }
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        playerData = DataManager.Instance.savePlayerData;
        anim = GetComponent<Animator>();
    }

    public void ChangeEquip()
    {
        anim.SetTrigger("ChangeEquip");
    }
    public void IsHit()
    {
        anim.SetTrigger("IsHit");
    }
    public void IsAttack()
    {
        anim.SetTrigger("IsAttack");
    }
    public void IsVictory()
    {
        anim.SetTrigger("IsVictory");
    }
    public void IsDie()
    {
        anim.SetTrigger("IsDie");
    }
}
