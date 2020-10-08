using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    Transform EnemyPos;

    private void Start()
    {
        EnemyPos = GameObject.Find("EnemyPos").GetComponent<Transform>();
        EnemyManager.Instance.RandomEnemy(EnemyPos);
    }
}
