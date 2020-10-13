using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{
    public DataManager dataManager;


    public void OnBattleScene()
    {
        dataManager.SaveCloth();
        SceneManager.LoadScene("TestHoon");
    }


}
