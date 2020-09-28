using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCamera : MonoBehaviour
{
    [SerializeField]
    float offSetX = -2;
    [SerializeField]
    float offSetY = 4;
    [SerializeField]
    float offSetZ = -1;
    public GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x+ offSetX, player.transform.position.y + offSetY, player.transform.position.z+ offSetZ);
    }
}
