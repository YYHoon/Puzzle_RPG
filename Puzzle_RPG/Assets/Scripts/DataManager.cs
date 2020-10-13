using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    [SerializeField]
    ChangeCloth.PlayerIdx saveIdx;
    public GameObject ClothIdx;
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
}
