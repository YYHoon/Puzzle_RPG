using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BGM : MonoBehaviour
{
    GameObject soundMg;

    void Start()
    {
        soundMg = transform.Find("Sound").gameObject;
        soundMg.SetActive(false);
    }
    
    public void OnSetting()
    {
        soundMg.SetActive(true);
    }

    public void EndSetting()
    {
        soundMg.SetActive(false);
    }
}
