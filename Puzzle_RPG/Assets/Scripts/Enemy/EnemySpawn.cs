using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    GameObject Enemy;
    Transform EnemyPos;

    private void Start()
    {
        EnemyPos = GameObject.Find("EnemyPos").GetComponent<Transform>();
        Enemy = EnemyManager.Instance.RandomEnemy();

        Instantiate(Enemy, EnemyPos.position, EnemyPos.rotation, EnemyPos);
        EnemyPos.localScale = new Vector3(2.0f, 2.0f, 1.0f);
    }
}
