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
    int shape = 0;

    public List<Enemy> EnemyList { get { return enemyList; } }
    public int Shape { get { return shape; } }

    private void Awake()
    {
        instance = this;
    }

    //에너미 4종류 리스트에 담기
    //public void SetEnemy()
    //{
    //    for (int i = 0; i < 4; i++)
    //    {
    //        Enemy enemy = new Enemy();
    //        enemy.Object = selectType(i);
    //        enemy.Type = (ENEMYTYPE)i;
    //        enemy.Initialize();
    //        enemyList.Add(enemy);
    //    }

    //    DataManager.Instance.SaveEnemy();
    //}

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

        shape = Random.Range(0, Enemies.Length);
        return Enemies[shape];
    }

    //에너미 생성
    public void spawn(Vector3 position)
    {       
        GameObject temp = selectType(index);

        GameObject enemy = Instantiate(temp, position, Quaternion.Euler(0, 0, 0));
        ENEMYTYPE type = (ENEMYTYPE)index;

        enemy.GetComponent<Enemy>().Initialize(type);
        enemyList.Add(enemy.GetComponent<Enemy>());
        
        DataManager.Instance.SaveEnemy(index, shape);
        index++;
    }
}
