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
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
   
    private void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x+ offSetX, player.transform.position.y + offSetY, player.transform.position.z+ offSetZ);
    }
}
