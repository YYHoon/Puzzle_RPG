using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCloth : MonoBehaviour
{
    public GameObject[] cloth;
    public Button rightButton;
    public Button leftButton;
    int clothIndex;
    // Start is called before the first frame update

    private void Start()
    {
        clothIndex = Random.Range(0, cloth.Length);
        cloth[clothIndex].SetActive(true);
    }

    public void OnRightButton(bool isRight)
    {
        // for(int i=0;i<cloth.Length; ++i)
        // {
        //     if(cloth[i].activeSelf==true)
        //     {
        //         Debug.Log("체크");
        //         cloth[i].SetActive(false);
        //         if (i + 1 == cloth.Length)
        //         {
        //             cloth[0].SetActive(true);
        //         }else
        //         {
        //             cloth[i + 1].SetActive(true);
        //         }
        //         break;
        //     }
        // }
        
        cloth[clothIndex].SetActive(false);
        if (isRight == false)
        {
            clothIndex--;
        }
        else
        {
            clothIndex++;
            clothIndex %= cloth.Length;
        }
        cloth[clothIndex].SetActive(true);

    }
}
