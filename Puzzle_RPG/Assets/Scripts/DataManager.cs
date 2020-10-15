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

    int[] Shape =new int[4];
    public int[] EnemyShape { get { return Shape; } }
    

    List<Enemy> enemyList = new List<Enemy>();
    public List<Enemy> EnemyList
    {
        get
        {
            return enemyList;
            //return EnemyManager.Instance.EnemyList;
        }
    }
    public int EnemyIdx
    {
        get;
        set;
    }

    public int[,] mapSave;
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

    public void SaveEnemy(int index, int shape)
    {
        enemyList = EnemyManager.Instance.EnemyList;
        Shape[index] = shape;
    }

    public void SaveMap(int [,]map)
    {
        mapSave = map;
    }
}
