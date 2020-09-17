using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFollowCamera : MonoBehaviour
{
    public Transform target;
    Transform tr;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        tr.position = new Vector3(target.position.x - 0.52f, tr.position.y, target.position.z - 6.56f);
        tr.LookAt(target);
    }
}
