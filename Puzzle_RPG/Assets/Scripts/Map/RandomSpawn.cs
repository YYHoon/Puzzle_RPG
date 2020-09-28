using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public GameObject spawnObject;
    public GameObject[] treeObject;
    public GameObject treeParent;
    private void Start()
    {
        
    }

    public void SpawnGameObject(int[,] map)
    {
        //좌상
        while (true)
        {
            int x = Random.Range(0, (int)(map.GetLength(0)*0.3f));
            int y = Random.Range(0, (int)(map.GetLength(1) * 0.3f));
            if (map[x, y] == 0&& checkMap(map,x,y))
            {
                Vector3 spawnPos = new Vector3(x- map.GetLength(0)*0.5f, 0.3f, y- map.GetLength(1) * 0.5f);
                Instantiate(spawnObject, spawnPos, Quaternion.Euler(0, 0, 0));
                break;
            }
        }
        //우상
        while (true)
        {
            int x = Random.Range((int)(map.GetLength(0) * 0.7f), map.GetLength(0) - 1);
            int y = Random.Range(0, (int)(map.GetLength(1) * 0.3f));
            if (map[x, y] == 0 && checkMap(map, x, y))
            {
                Vector3 spawnPos = new Vector3(x - map.GetLength(0) * 0.5f, 0.3f, y - map.GetLength(1) * 0.5f);
                Instantiate(spawnObject, spawnPos, Quaternion.Euler(0, 0, 0));
                break;
            }
        }
        //좌하
        while (true)
        {
            int x = Random.Range(0, (int)(map.GetLength(0) * 0.3f));
            int y = Random.Range((int)(map.GetLength(1) * 0.7f), map.GetLength(1));
            if (map[x, y] == 0 && checkMap(map, x, y))
            {
                Vector3 spawnPos = new Vector3(x - map.GetLength(0) * 0.5f, 0.3f, y - map.GetLength(1) * 0.5f);
                Instantiate(spawnObject, spawnPos, Quaternion.Euler(0, 0, 0));
                break;
            }
        }
        //우하
        while (true)
        {
            int x = Random.Range((int)(map.GetLength(0) * 0.7f), map.GetLength(0) - 1);
            int y = Random.Range((int)(map.GetLength(1) * 0.7f), map.GetLength(1));
            if (map[x, y] == 0 && checkMap(map, x, y))
            {
                Vector3 spawnPos = new Vector3(x - map.GetLength(0) * 0.5f, 0.3f, y - map.GetLength(1) * 0.5f);
                Instantiate(spawnObject, spawnPos, Quaternion.Euler(0, 0, 0));
                break;
            }
        }
    }

    bool checkMap(int[,] map,int x,int y)
    {
        for(int i=-1;i<=1;++i)
        {
            for(int j=-1;j<=1;++j)
            {
                if (i == 0 && j == 0) continue;
                if (x + i <= 0 || x + i >= map.GetLength(0) || y + i <= 0 || y + i >= map.GetLength(1)) return false;
                if (map[x+i, x+j] == 1) return false;
            }
        }
        return true;
    }

    public void TreeSpawn(int[,] map)
    {
        for (int i=0;i<map.GetLength(0);++i)
        {
            for(int j=0;j<map.GetLength(0);++j)
            {
                if(map[i,j]==1&&Random.Range(0,100)>88)
                {
                    GameObject newObject = Instantiate(treeObject[Random.Range(0,treeObject.Length)], new Vector3(i - map.GetLength(0) * 0.5f, 0.8f, j - map.GetLength(1) * 0.5f),Quaternion.Euler(0,0,0), treeParent.transform);
                    newObject.isStatic = true;
                }
            }
        }
    }

    public void DeleteChilds()
    {
        Transform[] child = treeParent.GetComponentsInChildren<Transform>();

        if(child != null)
        foreach(var iter in child)
        {
            if (iter != treeParent.transform) Destroy(iter.gameObject);
        }
    }
}
