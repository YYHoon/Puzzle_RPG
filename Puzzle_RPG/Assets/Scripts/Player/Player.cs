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
    Item[] currentItemData;
    Animator anim;
    [SerializeField]
    List<Transform> weaponList = new List<Transform>();
    [SerializeField]
    List<Transform> shieldList = new List<Transform>();
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
        currentItemData = GameObject.Find("DataManager").GetComponent<ItemData>().PlayerItemList();
        PlayerEquipment();
    }

    void PlayerEquipment()
    {
        // 무기 
        for (int i = 0; i < currentItemData.Length; ++i)
        {
            if (currentItemData[i] == null) continue;
            if (currentItemData[i].isUsing == true && currentItemData[i].type == "Weapon")
            {
                weaponList[currentItemData[i].idx].gameObject.SetActive(true);
                break;
            }
        }
        // 방패
        for (int i = 0; i < currentItemData.Length; ++i)
        {
            if (currentItemData[i] == null) continue;
            if (currentItemData[i].isUsing == true && currentItemData[i].type == "Shield")
            {
                shieldList[currentItemData[i].idx].gameObject.SetActive(true);
                break;
            }
        }
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
