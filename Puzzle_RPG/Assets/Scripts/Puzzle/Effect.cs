using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    ParticleSystem PS;
    ParticleSystem.Particle particle;
    RectTransform rtTransform;
    Vector3 startPos;
    Vector3 endPos;

    public void Initialize(RectTransform start)
    {        
        rtTransform = GetComponent<RectTransform>();
        startPos = start.position;
    }
}
