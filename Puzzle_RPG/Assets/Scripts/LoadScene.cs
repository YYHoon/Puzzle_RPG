using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [Header("로딩 관련")]
    [SerializeField] Image fadeImage;
    [SerializeField] Slider loadingSlider;
    public static string nextScene;
    float FadeTime = 2f;

    private void Start()
    {
        StartCoroutine(LoadingScene());
    }

    public static void LoadingScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

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

    public void ReloadPuzzleScene()
    {
        SceneManager.LoadScene("Puzzle");
    }

    public void ToHome()
    {
        Destroy(DataManager.Instance.gameObject);
        SceneManager.LoadScene("CharacterSelect");
    }

    IEnumerator LoadingScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
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

    IEnumerator FadeIn()
    {
        Debug.Log("fade in");
        Color fadeColor = fadeImage.color;
        float time = 0f;

        while (fadeColor.a < 1f)
        {
            //시간이 지남에 따라 색 진하게 하기
            time += Time.deltaTime / FadeTime;
            fadeColor.a = Mathf.Lerp(0f, 1f, time);
            yield return null;
            //yield return new WaitForSeconds(0.2f);
        }

        if (fadeColor.a >= 1f) fadeColor.a = 1f;
    }

    IEnumerator FadeOut()
    {
        Debug.Log("fade out");
        Color fadeColor = fadeImage.color;
        float time = 0f;

        while (fadeColor.a > 0f)
        {
            //시간이 지남에 따라 색 연하게 하기
            //fadeColor.a -= Time.deltaTime / FadeTime;
            time += Time.deltaTime / FadeTime;
            fadeColor.a = Mathf.Lerp(0f, 1f, time);
            yield return null;
            //yield return new WaitForSeconds(0.2f);
        }

        if (fadeColor.a <= 0f) fadeColor.a = 0f;
    }
}
