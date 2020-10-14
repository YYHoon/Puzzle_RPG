using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    public GameObject enemyObject;
    public ENEMYTYPE enemyType;

    Animator anim;

    float currentHp = 200f;
    float maxHp = 200f;
    public float enemyHp;

    public GameObject Object { get { return enemyObject; } set { enemyObject = value; } }
    public ENEMYTYPE Type { get { return enemyType; } set { enemyType = value; } }
    public float Hp { get { return enemyHp; } }

    //에너미 속성대로 이름 뜨게 하기
    public void EnemyTypeToString()
    {
        this.enemyObject.transform.name = enemyType.ToString();
    }

    //에너미가 공격 당할 때
    public void Damage(float damage)
    {
        if (currentHp <= 0)
        {
            anim.SetTrigger("IsDie");
            return;
        }

        currentHp -= damage;
        enemyHp = currentHp / maxHp;
    }
    
    //에너미가 공격할 때
    public void Attack()
    {
        anim.SetBool("IsAttack", true);
    }
}
