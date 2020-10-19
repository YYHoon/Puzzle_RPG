using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [Header("로딩 관련")]
    [SerializeField] Slider loadingSlider;
    public static string nextScene;

    public void SelectToBattle()
    {
        DataManager.Instance.SaveCloth();
        SceneManager.LoadScene("LoadingScene");
    }

    private void Start()
    {
        //LoadScene.LoadingScene("Battle");
        StartCoroutine(LoadingScene());
    }

    //public void BattleToPuzzle()
    //{
    //    LoadScene.LoadingScene("Puzzle");
    //}

    //public void PuzzleToBattle()
    //{
    //    LoadScene.LoadingScene("Battle");
    //}

    IEnumerator LoadingScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Battle");
        operation.allowSceneActivation = false;
        float time = 0f;

        while (!operation.isDone)
        {
            time += Time.deltaTime;

            if (operation.progress < 0.9f)
            {
                loadingSlider.value = Mathf.Lerp(loadingSlider.value, operation.progress, time);

                if (loadingSlider.value >= operation.progress)
                {
                    time = 0f;
                }
            }

            else
            {
                loadingSlider.value = Mathf.Lerp(loadingSlider.value, 1f, time);

                if (loadingSlider.value == 1f)
                {
                    operation.allowSceneActivation = true;
                }
                yield return new WaitForSeconds(0.05f);
            }
            yield return null;
        }
    }

}
