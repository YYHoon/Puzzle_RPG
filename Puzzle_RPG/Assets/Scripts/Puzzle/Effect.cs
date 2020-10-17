using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    ParticleSystem.Particle particle;
    RectTransform rtTransform;
    Vector3 startPos;
    Vector3 endPos;

    public void Initialize()
    {
        
        rtTransform = GetComponent<RectTransform>();
        startPos = rtTransform.position;
    }
}
