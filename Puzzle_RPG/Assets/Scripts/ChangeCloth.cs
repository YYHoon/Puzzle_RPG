using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCloth : MonoBehaviour
{
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
    int clothIndex;
    int hairIndex;
    int helmIndex;
    int gloveIndex;
    int shoesIndex;
    // Start is called before the first frame update

    private void Start()
    {
        clothChild.AddRange(cloth.GetComponentsInChildren<Transform>(true));
        clothChild.RemoveAt(0);
        clothIndex = Random.Range(0, clothChild.Count);
        clothChild[clothIndex].gameObject.SetActive(true);

        hairChild.AddRange(hair.GetComponentsInChildren<Transform>(true));
        hairChild.RemoveAt(0);
        hairIndex = Random.Range(0, hairChild.Count);
        hairChild[hairIndex].gameObject.SetActive(true);

        helmChild.AddRange(helm.GetComponentsInChildren<Transform>(true));
        helmChild.RemoveAt(0);
        helmIndex = Random.Range(0, helmChild.Count);
        helmChild[helmIndex].gameObject.SetActive(true);

        gloveChild.AddRange(glove.GetComponentsInChildren<Transform>(true));
        gloveChild.RemoveAt(0);
        gloveIndex = Random.Range(0, gloveChild.Count);
        gloveChild[gloveIndex].gameObject.SetActive(true);

        shoesChild.AddRange(shoes.GetComponentsInChildren<Transform>(true));
        shoesChild.RemoveAt(0);
        shoesIndex = Random.Range(0, shoesChild.Count);
        shoesChild[shoesIndex].gameObject.SetActive(true);
    }

    public void OnClothButton(bool isRight)
    {
        clothChild[clothIndex].gameObject.SetActive(false);
       if (isRight == false)
       {
           clothIndex--;
           if (clothIndex < 0)
               clothIndex = clothChild.Count - 1;
       }
       else
       {
           clothIndex++;
           clothIndex %= clothChild.Count;
       }
        clothChild[clothIndex].gameObject.SetActive(true);
    }

    public void OnHairButton(bool isRight)
    {
        hairChild[hairIndex].gameObject.SetActive(false);
        if (isRight == false)
        {
            hairIndex--;
            if (hairIndex < 0)
                hairIndex = hairChild.Count - 1;
        }
        else
        {
            hairIndex++;
            hairIndex %= hairChild.Count;
        }
        hairChild[hairIndex].gameObject.SetActive(true);
    }

    public void OnHelmButton(bool isRight)
    {
        helmChild[helmIndex].gameObject.SetActive(false);
        if (isRight == false)
        {
            helmIndex--;
            if (helmIndex < 0)
                helmIndex = helmChild.Count - 1;
        }
        else
        {
            helmIndex++;
            helmIndex %= helmChild.Count;
        }
        helmChild[helmIndex].gameObject.SetActive(true);
    }

    public void OnGloveButton(bool isRight)
    {
        gloveChild[gloveIndex].gameObject.SetActive(false);
        if (isRight == false)
        {
            gloveIndex--;
            if (gloveIndex < 0)
                gloveIndex = gloveChild.Count - 1;
        }
        else
        {
            gloveIndex++;
            gloveIndex %= gloveChild.Count;
        }
        gloveChild[gloveIndex].gameObject.SetActive(true);
    }

    public void OnShoesButton(bool isRight)
    {
        shoesChild[shoesIndex].gameObject.SetActive(false);
        if (isRight == false)
        {
            shoesIndex--;
            if (shoesIndex < 0)
                shoesIndex = shoesChild.Count - 1;
        }
        else
        {
            shoesIndex++;
            shoesIndex %= shoesChild.Count;
        }
        shoesChild[shoesIndex].gameObject.SetActive(true);
    }
}
