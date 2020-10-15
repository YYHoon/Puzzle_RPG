using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Hit()
    {
        anim.SetBool("IsHit", true);
    }

    public void Attack()
    {
        anim.SetBool("IsAttack", true);
    }
}
