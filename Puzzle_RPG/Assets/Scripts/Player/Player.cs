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
    float playerHp, fireDamage, waterDamage, plantDamage, fireDefense, waterDefense, plantDefense;
}
public class Player : MonoBehaviour
{
    PlayerData playerData;
    // Start is called before the first frame update

    public PlayerData PlayerStat()
    {
        return playerData;
    }
}
