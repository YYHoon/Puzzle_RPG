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

    int random;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Enemies = Resources.LoadAll<GameObject>("Prefabs/Enemy/");
        
        //if (GameObject.FindWithTag("Fire"))
        //    Enemies = GameObject.FindGameObjectsWithTag("Fire");

        //else if (GameObject.FindWithTag("Water"))
        //    Enemies = Resources.LoadAll<GameObject>("Prefabs/Enemy/WaterEnemy/");

        //else if (GameObject.FindWithTag("Plant"))
        //    Enemies = Resources.LoadAll<GameObject>("Prefabs/Enemy/PlantEnemy/");

        //FireEnemy = Resources.LoadAll<GameObject>("Prefabs/Enemy/FireEnemy/");
        //WaterEnemy = Resources.LoadAll<GameObject>("Prefabs/Enemy/WaterEnemy/");
        //PlantEnemy = Resources.LoadAll<GameObject>("Prefabs/Enemy/PlantEnemy/");

    }

    public void RandomEnemy(Transform enemyPos)
    {
        random = Random.Range(0, Enemies.Length);
        Instantiate(Enemies[random], enemyPos);

        //Instantiate(FireEnemy[Random.Range(0, FireEnemy.Length)], enemyPos);
        //Instantiate(WaterEnemy[Random.Range(0, WaterEnemy.Length)], enemyPos);
        //Instantiate(PlantEnemy[Random.Range(0, PlantEnemy.Length)], enemyPos);
    }
}
