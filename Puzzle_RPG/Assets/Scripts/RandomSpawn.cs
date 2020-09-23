using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public GameObject spawnObject;
   
    public void SpawnGameObject(int[,] map)
    {
        int x = map.GetLength(0);
        int y = map.GetLength(1);
        if(map[y,x]==0)
        {
            
        }

        for (int i=0;i<map.GetLength(0);++i)
        {
            for(int j=0;j<map.GetLength(1);++j)
            {

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
