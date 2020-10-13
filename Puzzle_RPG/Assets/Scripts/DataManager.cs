using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    static DataManager instance;
    public static DataManager Instance { get { return instance; } }

    [SerializeField]
    ChangeCloth.PlayerIdx saveIdx;
    public GameObject ClothIdx;
    List<Enemy> enemyList = new List<Enemy>();
    public ChangeCloth.PlayerIdx SaveIdx
    {
        get
        {
            return saveIdx;
        }
        set
        {
            saveIdx = value;
        }
    }

    [Header("Enemy")]
    Enemy enemy;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);        
    }
    
    public void SaveCloth()
    {
        saveIdx = ClothIdx.GetComponent<ChangeCloth>().ClothIdx;
    }

    public void LoadCloth()
    {
        ClothIdx = GameObject.Find("Player_H1");
        Debug.Log(ClothIdx);
    }

    public void SaveEnemy()
    {
        enemyList = EnemyManager.Instance.EnemyList;        
    }

    public Enemy LoadEnemy(Enemy battleEnemy)
    {
        Enemy enemy = new Enemy();
        enemy = battleEnemy;
        return enemy;
    }
}
