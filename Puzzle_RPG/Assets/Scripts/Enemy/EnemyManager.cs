using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum ENEMYTYPE
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
    List<GameObject> enemyList = new List<GameObject>();
    GameObject[] Enemies;
    Transform EnemyPos;
    [SerializeField] ENEMYTYPE enemyType;

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
        //EnemyPos = GameObject.Find("EnemyPos").GetComponent<Transform>();

        RandomEnemy();
        //spawn();
    }

    private void Update()
    {
       // HpBar.value = currentHp / maxHp;
    }

    //에너미 랜덤 반환
    public GameObject RandomEnemy()
    {
        int random = Random.Range(0, 4);
        GameObject enemy = selectType(random);
        enemyType = (ENEMYTYPE)random;
        //Instantiate(Enemies[random], enemyPos.position, enemyPos.rotation);
        
        for (int i = 0; i < enemyList.Count; i++)
        {
            while (enemyList[random] == enemyList[i])
            {
                random = Random.Range(0, 4);
            }            
            
            enemy = selectType(random);
            enemyType = (ENEMYTYPE)random;
            enemyList.Add(enemy);
            break;            
        }        

        return enemy;
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
        GameObject enemy = RandomEnemy();

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
