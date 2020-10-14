using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ENEMYTYPE
{
    fire = 0,
    water = 1,
    plant = 2,
    evil = 3,
}

public class EnemyManager : MonoBehaviour
{
    static EnemyManager instance;
    public static EnemyManager Instance { get { return instance; } }

    [Header("Enemy")]
    List<Enemy> enemyList = new List<Enemy>();
    GameObject[] Enemies;
    int index = 0;

    public List<Enemy> EnemyList { get { return enemyList; } }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetEnemy();
    }

    //에너미 4종류 리스트에 담기
    public void SetEnemy()
    {
        for (int i = 0; i < 4; i++)
        {
            Enemy enemy = new Enemy();
            enemy.Object = selectType(i);
            enemy.Type = (ENEMYTYPE)i;
            enemy.EnemyTypeToString();
            enemyList.Add(enemy);
        }
        DataManager.Instance.SaveEnemy();
    }

    //에너미 프리팹 지정
    GameObject selectType(int type)
    {
        if (type == 0)
        {
            Enemies = Resources.LoadAll<GameObject>("Prefabs/Enemy/FireEnemy/");
        }

        else if (type == 1)
        {
            Enemies = Resources.LoadAll<GameObject>("Prefabs/Enemy/WaterEnemy/");
        }

        else if (type == 2)
        {
            Enemies = Resources.LoadAll<GameObject>("Prefabs/Enemy/PlantEnemy/");
        }

        else if (type == 3)
        {
            Enemies = Resources.LoadAll<GameObject>("Prefabs/Enemy/EvilEnemy/");
        }

        int random = Random.Range(0, Enemies.Length);
        return Enemies[random];
    }

    //에너미 생성
    public GameObject spawn(Vector3 position)
    {
        if (enemyList.Count <= 0) return null;

        GameObject enemy = enemyList[index].Object;
        index++;

        return Instantiate(enemy, position, Quaternion.Euler(0, 0, 0));        
    }
}
