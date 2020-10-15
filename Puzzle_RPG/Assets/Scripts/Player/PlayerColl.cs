using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerColl : MonoBehaviour
{
    public Button enemySearchButton;
    bool enemySearch = false;
    int enemyIdx = 0;
    private void Update()
    {
        if(enemySearch == true)
        {
            enemySearchButton.gameObject.SetActive(true);
        }
        else
        {
            enemySearchButton.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            for(int i = 0; i < DataManager.Instance.EnemyList.Count; ++i)
            {
                if (other.gameObject.name == DataManager.Instance.EnemyList[i].gameObject.name)
                {
                    DataManager.Instance.EnemyIdx = i;
                }
            }
            enemySearch = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
            enemySearch = false;
    }
}
