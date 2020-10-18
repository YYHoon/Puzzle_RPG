using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public void SelectToBattle()
    {
        DataManager.Instance.SaveCloth();
        LoadScene.LoadingScene("Battle");
    }

    public void BattleToPuzzle()
    {
        LoadScene.LoadingScene("Puzzle");
    }

    public void PuzzleToBattle()
    {
        LoadScene.LoadingScene("Battle");
    }
}
