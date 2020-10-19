using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    float bgmVolume = 0.5f;
    [SerializeField] AudioSource BGM;
    [SerializeField] Slider bgmBar;

    private void Start()
    {
        setVolume();
    }

    private void Update()
    {
        getVolume();
    }

    //처음 브금 볼륨 설정
    void setVolume()
    {
        bgmVolume = PlayerPrefs.GetFloat("bgmVoluem", bgmVolume);
        bgmBar.value = bgmVolume;
        BGM.volume = bgmBar.value;
    }

    //슬라이더 움직일 때마다 브금 설정
    void getVolume()
    {
        BGM.volume = bgmBar.value;
        bgmVolume = bgmBar.value;
        PlayerPrefs.SetFloat("bgmVolume", bgmVolume);
    }
}
