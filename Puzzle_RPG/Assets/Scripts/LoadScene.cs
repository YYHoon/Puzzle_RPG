using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [Header("페이드인 관련")]
    [SerializeField] Image fadeImage;
    float FadeTime = 2f;

    //[Header("로딩 관련")]
    //[SerializeField] Slider loadingSlider;
    //public static string nextScene;

    //private void Start()
    //{
    //    StartCoroutine(LoadingScene());
    //}

    //public static void LoadingScene(string sceneName)
    //{
    //    nextScene = sceneName;
    //    SceneManager.LoadScene("LoadingScene");
    //}

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void OnBattleScene()
    {
        DataManager.Instance.SaveCloth();
        SceneManager.LoadScene("Battle");
    }

    public void OnPuzzleScene()
    {
        DataManager.Instance.GetComponent<ItemData>().Save();
        StartCoroutine(FadeOut("Puzzle"));
        //DataManager.Instance.GetComponent<ItemData>().Save();
        //SceneManager.LoadScene("Puzzle");
    }
    
    public void FromPuzzleToBattle()
    {
        StartCoroutine(FadeOut("Battle"));
        //SceneManager.LoadScene("Battle");
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


    IEnumerator FadeOut(string sceneName)
    {
        fadeImage.enabled = true;
        Color fadeColor = fadeImage.color;
        fadeColor.a = 0f;
        fadeImage.color = fadeColor;
        float time = 0f;

        while (fadeColor.a < 1f)
        {
            //시간이 지남에 따라 색 진하게 하기
            time += Time.deltaTime;
            fadeColor.a = Mathf.Lerp(0f, 1f, time);
            fadeImage.color = fadeColor;
            yield return null;
            //yield return new WaitForSeconds(0.2f);
        }

        if (fadeColor.a >= 1f)
            fadeColor.a = 1f;

        SceneManager.LoadScene(sceneName);
    }

    public IEnumerator FadeIn()
    {
        fadeImage.enabled = true;
        Color fadeColor = fadeImage.color;
        fadeColor.a = 1f;
        fadeImage.color = fadeColor;
        float time = 0f;

        while (fadeColor.a > 0f)
        {
        Debug.Log(fadeColor.a);
            //시간이 지남에 따라 색 연하게 하기
            //fadeColor.a -= Time.deltaTime / FadeTime;
            time += Time.deltaTime / FadeTime;
            fadeColor.a = Mathf.Lerp(1f, 0f, time);
            fadeImage.color = fadeColor;
            yield return null;
            //yield return new WaitForSeconds(0.2f);
        }

        if (fadeColor.a <= 0f)
            fadeColor.a = 0f;
    }
}
