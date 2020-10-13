using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    static DataManager instance;
    public static DataManager Instance { get { return instance; } }

    ChangeCloth.PlayerIdx saveIdx;
    public GameObject ClothIdx;

    List<Enemy> enemyList = new List<Enemy>();
<<<<<<< HEAD
    public List<Enemy> EnemyList
    {
        get
        {
            return enemyList;
        }
    }
    public int EnemyIdx
    {
        get;
        set;
    }

=======
    public int[,] mapSave;
>>>>>>> origin/master
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
    }

    public void SaveEnemy()
    {
        enemyList = EnemyManager.Instance.EnemyList;
    }

    public void SaveMap(int [,]map)
    {
        mapSave = map;
    }
}
