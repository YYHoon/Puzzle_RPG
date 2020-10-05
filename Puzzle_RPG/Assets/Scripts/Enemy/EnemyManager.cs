using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    GameObject[] Enemies;
    Transform EnemyPos;
    int random;

    void Start()
    {
        Enemies = Resources.LoadAll<GameObject>("Prefabs/Enemy/");
        EnemyPos = GameObject.Find("EnemyPos").GetComponent<Transform>();
        RandomEnemy();
    }

    void RandomEnemy()
    {
        random = Random.Range(0, Enemies.Length);

        Instantiate(Enemies[random], EnemyPos);
    }
}
