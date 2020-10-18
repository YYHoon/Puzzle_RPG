using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCloth : MonoBehaviour
{
    public struct PlayerIdx
    {
      public int clothIndex;
      public int hairIndex;
      public int helmIndex;
      public int gloveIndex;
      public int shoesIndex;
    }
    public Transform cloth;
    List<Transform> clothChild = new List<Transform>();
    public Transform hair;
    List<Transform> hairChild = new List<Transform>();
    public Transform helm;
    List<Transform> helmChild = new List<Transform>();
    public Transform glove;
    List<Transform> gloveChild = new List<Transform>();
    public Transform shoes;
    List<Transform> shoesChild = new List<Transform>();
    PlayerIdx playerIdx;

    public PlayerIdx ClothIdx
    { get
        {
            return playerIdx;
        }
        set
        {
            playerIdx = value;
        }
    }
   
    // Start is called before the first frame update

    private void Start()
    {
        clothChild.AddRange(cloth.GetComponentsInChildren<Transform>(true));
        clothChild.RemoveAt(0);
        playerIdx.clothIndex = Random.Range(0, clothChild.Count);
        clothChild[playerIdx.clothIndex].gameObject.SetActive(true);

        hairChild.AddRange(hair.GetComponentsInChildren<Transform>(true));
        hairChild.RemoveAt(0);
        playerIdx.hairIndex = Random.Range(0, hairChild.Count);
        hairChild[playerIdx.hairIndex].gameObject.SetActive(true);

        helmChild.AddRange(helm.GetComponentsInChildren<Transform>(true));
        helmChild.RemoveAt(0);
        playerIdx.helmIndex = Random.Range(0, helmChild.Count);
        helmChild[playerIdx.helmIndex].gameObject.SetActive(true);

        gloveChild.AddRange(glove.GetComponentsInChildren<Transform>(true));
        gloveChild.RemoveAt(0);
        playerIdx.gloveIndex = Random.Range(0, gloveChild.Count);
        gloveChild[playerIdx.gloveIndex].gameObject.SetActive(true);

        shoesChild.AddRange(shoes.GetComponentsInChildren<Transform>(true));
        shoesChild.RemoveAt(0);
        playerIdx.shoesIndex = Random.Range(0, shoesChild.Count);
        shoesChild[playerIdx.shoesIndex].gameObject.SetActive(true);
    }
   
    public void OnClothButton(bool isRight)
    {
        clothChild[playerIdx.clothIndex].gameObject.SetActive(false);
       if (isRight == false)
       {
            playerIdx.clothIndex--;
           if (playerIdx.clothIndex < 0)
                playerIdx.clothIndex = clothChild.Count - 1;
       }
       else
       {
            playerIdx.clothIndex++;
            playerIdx.clothIndex %= clothChild.Count;
       }
        clothChild[playerIdx.clothIndex].gameObject.SetActive(true);
    }

    public void OnHairButton(bool isRight)
    {
        hairChild[playerIdx.hairIndex].gameObject.SetActive(false);
        if (isRight == false)
        {
            playerIdx.hairIndex--;
            if (playerIdx.hairIndex < 0)
                playerIdx.hairIndex = hairChild.Count - 1;
        }
        else
        {
            playerIdx.hairIndex++;
            playerIdx.hairIndex %= hairChild.Count;
        }
        hairChild[playerIdx.hairIndex].gameObject.SetActive(true);
    }

    public void OnHelmButton(bool isRight)
    {
        helmChild[playerIdx.helmIndex].gameObject.SetActive(false);
        if (isRight == false)
        {
            playerIdx.helmIndex--;
            if (playerIdx.helmIndex < 0)
                playerIdx.helmIndex = helmChild.Count - 1;
        }
        else
        {
            playerIdx.helmIndex++;
            playerIdx.helmIndex %= helmChild.Count;
        }
        helmChild[playerIdx.helmIndex].gameObject.SetActive(true);
    }

    public void OnGloveButton(bool isRight)
    {
        gloveChild[playerIdx.gloveIndex].gameObject.SetActive(false);
        if (isRight == false)
        {
            playerIdx.gloveIndex--;
            if (playerIdx.gloveIndex < 0)
                playerIdx.gloveIndex = gloveChild.Count - 1;
        }
        else
        {
            playerIdx.gloveIndex++;
            playerIdx.gloveIndex %= gloveChild.Count;
        }
        gloveChild[playerIdx.gloveIndex].gameObject.SetActive(true);
    }

    public void OnShoesButton(bool isRight)
    {
        shoesChild[playerIdx.shoesIndex].gameObject.SetActive(false);
        if (isRight == false)
        {
            playerIdx.shoesIndex--;
            if (playerIdx.shoesIndex < 0)
                playerIdx.shoesIndex = shoesChild.Count - 1;
        }
        else
        {
            playerIdx.shoesIndex++;
            playerIdx.shoesIndex %= shoesChild.Count;
        }
        shoesChild[playerIdx.shoesIndex].gameObject.SetActive(true);
    }
}
