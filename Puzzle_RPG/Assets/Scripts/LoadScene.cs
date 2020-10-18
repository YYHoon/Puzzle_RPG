using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public Transform EnemyPosition;

    public void OnBattleScene()
    {
        DataManager.Instance.SaveCloth();
        SceneManager.LoadScene("Battle");
    }

    public void OnPuzzleScene()
    {
        DataManager.Instance.GetComponent<ItemData>().Save();
        SceneManager.LoadScene("Puzzle");
    }
    
    public void FromPuzzleToBattle()
    {
        SceneManager.LoadScene("Battle");
    }
}
