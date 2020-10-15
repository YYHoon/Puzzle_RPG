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
    Enemy puzzleEnemy = new Enemy();
    float damage = 0;   //에너미가 받는 데미지
    int turn;           //에너미 공격 턴수 계산용
    int random;         //에너미 공격 턴수 랜덤 부가용

    float playerHp = 100f;

    [SerializeField] Slider EnemyHpBar;
    [SerializeField] Slider PlayerHpBar;

    List<Enemy> enemyList = new List<Enemy>();
    public List<Enemy> EnemyList { get { return enemyList; } }
    
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        enemyPos = GetComponent<Transform>();
        SetEnemy();
    }

    private void Update()
    {
        //플레이어 턴 하나 끝나면
        if (GameBoard.Instance.PlayerTurn == true)
        {
            //에너미 체력바 갱신
            puzzleEnemy.Damage(damage);
            EnemyHpBar.value = puzzleEnemy.Hp;
            //초기화
            damage = 0;
            turn++;
            Debug.Log("turn : " + turn + " / random : " + random);

            //에너미 공격 턴 돌아오면
            if (EnemyAttack())
            {
                //에너미 공격시키고
                puzzleEnemy.Attack();
                playerHp -= 10f;
                PlayerHpBar.value = playerHp / 100f;
                //턴 수 다시 뽑아보기
                puzzleEnemy.WaitAtk(0);
                random = Random.Range(1, 4);
                Debug.Log("new random : " + random);
                turn = 0;
            }
            GameBoard.Instance.PlayerTurn = false;
        }
    }

    //데이터매니저에서 정보 받아온 에너미 퍼즐씬에 세팅
    void SetEnemy()
    {
        int index = DataManager.Instance.EnemyIdx;
        int shape = DataManager.Instance.EnemyShape[index];

        GameObject temp = selectType(index, shape);
        GameObject enemy = Instantiate(temp, enemyPos.position, enemyPos.rotation, enemyPos);
        enemyPos.localScale = new Vector3(2.0f, 2.0f, 1.0f);

        ENEMYTYPE type = (ENEMYTYPE)index;
        puzzleEnemy = enemy.GetComponent<Enemy>();
        puzzleEnemy.Initialize(type);
        random = Random.Range(1, 4);

        ///enemy.Object = enemyList[index].Object;
        ///enemy.Type = enemyList[index].Type;
        ///Instantiate(enemy.Object, enemyPos.position, enemyPos.rotation, enemyPos);
    }

    //에너미 프리팹 지정
    GameObject selectType(int type, int shape)
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

        //int random = Random.Range(0, Enemies.Length);
        return Enemies[shape];
    }

    //에너미 맞았당
    public void EnemyHit(Attack attack)
    {
        //Debug.Log("normal : " + (attack.fire + attack.water + attack.plant));

        //넘겨받은 구조체로 에너미 속성별로 다시 계산
        if (puzzleEnemy.Type == ENEMYTYPE.fire)
        {
            damage += attack.fire + attack.water * 2 + attack.plant;
            //Debug.Log("fire : " + damage);
        }

        else if (puzzleEnemy.Type == ENEMYTYPE.water)
        {
            damage += attack.fire + attack.water + attack.plant * 2;
            //Debug.Log("water : " + damage);
        }

        else if (puzzleEnemy.Type == ENEMYTYPE.plant)
        {
            damage += attack.fire * 2 + attack.water + attack.plant;
            //Debug.Log("plant : " + damage);
        }

        else if (puzzleEnemy.Type == ENEMYTYPE.evil)
        {
            damage += (attack.fire + attack.water + attack.plant) * 0.7f;
            //Debug.Log("evil : " + damage);
        }
    }

    public bool EnemyAttack()
    {
        if (turn == random - 1)
            puzzleEnemy.WaitAtk(1);

        else if (turn == random)
        {
            Debug.Log("에너미 턴!");
            return true;
        }
        return false;
    }
}
