using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{
    public void OnBattleScene()
    {
        DataManager.Instance.SaveCloth();
        SceneManager.LoadScene("Battle");
    }

    public void OnPuzzleScene()
    {
        DataManager.Instance.SaveEnemy();
        SceneManager.LoadScene("TestPuzzle");
    }

    
}
