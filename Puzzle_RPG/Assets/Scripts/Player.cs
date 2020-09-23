using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed = 5.0f;
    Vector3 forward, right;

    // Start is called before the first frame update
    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0f;
        forward = Vector3.Normalize(forward);

        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey)
        {
            Move();
        }
    }
    private void Move()
    {
        Vector3 RightMovement = right * speed * Time.smoothDeltaTime * Input.GetAxis("Horizontal");
        Vector3 ForwardMovement = forward * speed * Time.smoothDeltaTime * Input.GetAxis("Vertical");

        Vector3 FinalMovement = ForwardMovement + RightMovement;

        Vector3 directon = Vector3.Normalize(FinalMovement);

        if(directon != Vector3.zero)
        {
            transform.forward = directon;
            transform.position += FinalMovement;
        }
    }
}
