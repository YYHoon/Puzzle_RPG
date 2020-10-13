using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
    static EnemySpawn instance;
    public static EnemySpawn Instance { get { return instance; } }

    public Transform enemyPos;
    GameObject[] Enemies;
    Enemy enemy = new Enemy();
    
    [SerializeField] Slider HpBar;

    List<Enemy> enemyList = new List<Enemy>();
    public List<Enemy> EnemyList { get { return enemyList; } }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        enemyPos = GetComponent<Transform>();
        enemyList = DataManager.Instance.EnemyList;
        SetEnemy();
    }

    void SetEnemy()
    {
        int index = DataManager.Instance.EnemyIdx;        
        enemy.Object = enemyList[index].Object;
        enemy.Type = enemyList[index].Type;
        //int random = Random.Range(0, 4);
        //enemy.Object = selectType(random);
        //enemy.Type = (ENEMYTYPE)random;
        enemy.EnemyTypeToString();

        Instantiate(enemy.Object, enemyPos.position, enemyPos.rotation, enemyPos);
        enemyPos.localScale = new Vector3(2.0f, 2.0f, 1.0f);
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

    //에너미 맞았당
    public void EnemyHit(Attack attack)
    {
        float damage = 0;
        //Debug.Log("normal : " + (attack.fire + attack.water + attack.plant));

        //에너미 데미지 주고        
        if (enemy.Type == ENEMYTYPE.fire)
        {
            damage = attack.fire + attack.water * 2 + attack.plant;
            //Debug.Log("fire : " + damage);
        }

        else if (enemy.Type == ENEMYTYPE.water)
        {
            damage = attack.fire + attack.water + attack.plant * 2;
            //Debug.Log("water : " + damage);
        }

        else if (enemy.Type == ENEMYTYPE.plant)
        {
            damage = attack.fire * 2 + attack.water + attack.plant;
            //Debug.Log("plant : " + damage);
        }

        else if (enemy.Type == ENEMYTYPE.evil)
        {
            damage = (attack.fire + attack.water + attack.plant) * 0.7f;
            //Debug.Log("evil : " + damage);
        }

        //에너미 체력바 갱신
        enemy.Damage(damage);
        HpBar.value = enemy.Hp;
    }
}
