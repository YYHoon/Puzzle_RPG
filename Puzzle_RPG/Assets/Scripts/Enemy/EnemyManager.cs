using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    static EnemyManager instance;
    public static EnemyManager Instance { get { return instance; } }

    [SerializeField] GameObject[] Enemies;
    //GameObject[] FireEnemy;
    //GameObject[] WaterEnemy;
    //GameObject[] PlantEnemy;
    
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Enemies = Resources.LoadAll<GameObject>("Prefabs/Enemy/");

        //if (GameObject.FindWithTag("Fire"))
        //    Enemies = GameObject.FindGameObjectsWithTag("Fire");
        //FireEnemy = Resources.LoadAll<GameObject>("Prefabs/Enemy/FireEnemy/");
    }

    //에너미 랜덤 생성 후 반환
    public GameObject RandomEnemy()
    {
        int random = Random.Range(0, Enemies.Length);
        //Instantiate(Enemies[random], enemyPos.position, enemyPos.rotation);

        return Enemies[random];
    }
}
