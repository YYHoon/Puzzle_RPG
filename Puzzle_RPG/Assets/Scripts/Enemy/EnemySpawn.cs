using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
    static EnemySpawn instance;
    public static EnemySpawn Instance { get { return instance; } }

    [Header("에너미 생성하는 데 필요함")]
    public Transform enemyPos;
    [SerializeField] Player player;
    [SerializeField] ParticleSystem playerHealing;
    [SerializeField] ParticleSystem playerHit;
    GameObject[] Enemies;
    Enemy puzzleEnemy = new Enemy();

    [Header("체력바 관련")]
    float damage = 0;       //에너미가 받는 데미지
    float healing = 0;
    float playerHp = 100f;  //플레이어 전체 체력
    [SerializeField] Slider EnemyHpBar;
    [SerializeField] Slider PlayerHpBar;

    [Header("에너미 턴UI 관련")]
    [SerializeField] Image[] turn;  //턴수 보여줄 이미지
    [SerializeField] ParticleSystem enemyHit;
    int enemyTurn;                  //에너미가 공격할 턴수

    [SerializeField] GameObject gameOver;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        enemyPos = GetComponent<Transform>();
        SetEnemy();
        if (enemyTurn == 1)
            puzzleEnemy.WaitAtk(1);
        else
            puzzleEnemy.WaitAtk(0);
    }

    private void Update()
    {
        //플레이어 턴 하나 끝나면
        if (GameBoard.Instance.PlayerTurn == true)
        {
            if(healing>0)
            {
                playerHealing.Play();
                if (playerHp + healing >= 100f) playerHp = 100f;
                    else playerHp += healing;    
                PlayerHpBar.value = playerHp / 100f;
                healing = 0;
            }
            //에너미 체력바 갱신
            if (damage != 0)
            {
                puzzleEnemy.Damage(damage);
                player.IsAttack();
                enemyHit.Play();
            }
            EnemyHpBar.value = puzzleEnemy.Hp;

            //데미지 초기화
            damage = 0;
           
            //에너미 공격 턴 돌아오면
            EnemyAttack();
            GameBoard.Instance.PlayerTurn = false;
        }
    }

    //데이터매니저에서 정보 받아온 에너미 퍼즐씬에 세팅
    void SetEnemy()
    {
        //데이터 매니저에서 에너미(오브젝트)
        //인덱스(tyep)와 종류(shape) 받아오기
        int index = DataManager.Instance.EnemyIdx;
        int shape = DataManager.Instance.EnemyShape[index];

        //생성 후 크기 조절
        GameObject temp = selectType(index, shape);
        GameObject enemy = Instantiate(temp, enemyPos.position, enemyPos.rotation, enemyPos);
        enemyPos.localScale = new Vector3(2.0f, 2.0f, 1.0f);

        //타입(shape)대로 에너미 대입
        ENEMYTYPE type = (ENEMYTYPE)index;
        puzzleEnemy = enemy.GetComponent<Enemy>();
        puzzleEnemy.Initialize(type);
        enemyTurn = Random.Range(1, 4);

        //에너미 턴수 알려주는 UI 이미지 켜기
        for (int i = 0; i < enemyTurn; i++)
            turn[i].enabled = true;

        ///enemy.Object = enemyList[index].Object;
        ///enemy.Type = enemyList[index].Type;
        ///Instantiate(enemy.Object, enemyPos.position, enemyPos.rotation, enemyPos);
    }

    //에너미 프리팹 지정
    public GameObject selectType(int type, int shape)
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
        if (attack.heal > 0)
        {
            healing += attack.heal;
        }
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

    //에너미 공격한당
    public void EnemyAttack()
    {
        //턴수대로 UI 이미지 끄기
        turn[enemyTurn - 1].enabled = false;
        enemyTurn--;

        //공격 전에 공격 준비 아이들
        if (enemyTurn == 1)
        {
            puzzleEnemy.WaitAtk(1);
        }

        else if (enemyTurn == 0)
        {
            //에너미 공격 애니메이션
            StartCoroutine(AttackAfterHit());
        }
    }

    //맞은 뒤 공격 애니메이션 순차적으로 돌리기 위한 코루틴
    IEnumerator AttackAfterHit()
    {
        yield return new WaitForSeconds(1);

        //에너미 공격 시키기
        puzzleEnemy.Attack();

        PlayerDefenseAttack();
        PlayerHpBar.value = playerHp / 100f;
        playerHit.Play();
        player.IsHit();
        yield return new WaitForSeconds(1f);
        if (playerHp <= 0)
        {
            player.IsDie();
            puzzleEnemy.Victory();
            yield return new WaitForSeconds(3f);
            gameOver.SetActive(true);
        }
        //턴 수 다시 뽑아서 UI 켜기
        enemyTurn = Random.Range(1, 4);
        for (int i = 0; i < enemyTurn; i++)
            turn[i].enabled = true;
        
        yield return new WaitForSeconds(0.2f);

        //턴수에 따라 아이들 상태 정해주기
        if (enemyTurn == 1)
            puzzleEnemy.WaitAtk(1);
        else
            puzzleEnemy.WaitAtk(0);
    }

    void PlayerDefenseAttack()
    {
        if (puzzleEnemy.Type == ENEMYTYPE.fire)
        {
            playerHp -= (2 - DataManager.Instance.savePlayerData.fireDefense) * 10.0f;
        }

        else if (puzzleEnemy.Type == ENEMYTYPE.water)
        {
            playerHp -= (2 - DataManager.Instance.savePlayerData.waterDefense) * 10.0f;
        }

        else if (puzzleEnemy.Type == ENEMYTYPE.plant)
        {
            playerHp -= (2 - DataManager.Instance.savePlayerData.plantDefense) * 10.0f;
        }

        else if (puzzleEnemy.Type == ENEMYTYPE.evil)
        {
            playerHp -= 15f;
        }
    }
}
