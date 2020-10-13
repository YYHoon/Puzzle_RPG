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
    Transform EnemyPos;
    //ENEMYTYPE enemyType;

    [Header("HpBar")]
    [SerializeField] Slider HpBar;
    float currentHp = 100f;
    float maxHp = 100f;

    //public ENEMYTYPE enemytype { get { return enemyType; } }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetEnemy();
    }

    private void Update()
    {
       // HpBar.value = currentHp / maxHp;
    }

    //에너미 랜덤 반환
    public void SetEnemy()
    {
        //int random = Random.Range(0, 4);
        //Enemy enemy = new Enemy();
        //enemy.Type = (ENEMYTYPE)random;
        //
        //if (enemyList.Count != 0)
        //{
        //    for (int i = 0; i < enemyList.Count; i++)
        //    {
        //        //if (enemy.Type != enemyList[i].Type) continue;
        //
        //        while (enemy.Type == enemyList[i].Type)
        //        {
        //            random = Random.Range(0, 4);
        //            enemy.Type = (ENEMYTYPE)random;
        //        }
        //    }
        //}        
        //
        //enemy.Object = selectType(random);
        //enemyList.Add(enemy);

        for (int i = 0; i < 4; i++)
        {
            Enemy enemy = new Enemy();
            enemy.Object = selectType(i);
            enemy.Type = (ENEMYTYPE)i;
            enemyList.Add(enemy);
            enemy.EnemyTypeToString();
        }
    }

    //에너미 타입 지정
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

        GameObject enemy = enemyList[0].Object;
        enemyList.RemoveAt(0);

        //Instantiate(enemy, EnemyPos.position, EnemyPos.rotation, EnemyPos);
        //EnemyPos.localScale = new Vector3(2.0f, 2.0f, 1.0f);

        return Instantiate(enemy, position, Quaternion.Euler(0, 0, 0));        
    }

    //에너미가 공격 당할 때
    void Damage(float damage)
    {
        currentHp -= damage;
    }
}

public class Enemy
{
   public GameObject enemyObject;
   public ENEMYTYPE enemyType;

    public GameObject Object { get {return enemyObject;} set { enemyObject = value; } }
    public ENEMYTYPE Type { get { return enemyType; } set { enemyType = value; } }

    public void EnemyTypeToString()
    {
        this.enemyObject.transform.name = enemyType.ToString();
    }
}
