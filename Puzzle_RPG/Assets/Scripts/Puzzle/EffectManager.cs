using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    static EffectManager instance;
    public static EffectManager Instance { get { return instance; } }
    
    [Header("이펙트 생성 관련")]
    [SerializeField] GameObject[] effects = new GameObject[4];
    Effect PuzzleEffect = new Effect();

    [Header("이펙트 이동 관련")]
    [SerializeField] RectTransform[] destinations = new RectTransform[4];
    [SerializeField] Transform enemyPos;
    Vector3 startPos;
    Vector3 endPos;

    private void Awake()
    {
        instance = this;
    }

    public void CreateEffect(int index, RectTransform start)
    {
        GameObject shape = effects[index];
        GameObject effect = Instantiate(shape, start);
        
        Effect puzzleEffect = effect.GetComponent<Effect>();
        Debug.Log(puzzleEffect.name + " : " + puzzleEffect.transform.position);
        puzzleEffect.Initialize(start);
    }

    //생성 후 모이는 위치로 가는 함수
    public void PuzzlePop()
    {
        RectTransform form = GetComponent<RectTransform>();
        Vector3 point;

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(form, PuzzleEffect.transform.position, Camera.current, out point))
        {
            Vector3 move = point - endPos;
            Vector3 normalMove = move.normalized;

            float goal = move.magnitude;

            if (goal > 0f)
            {
                PuzzleEffect.transform.Translate(endPos * 16f * Time.deltaTime);
            }

            else
            {
                PuzzleEffect.transform.position = endPos;
            }
        }

        transform.Translate(endPos * Time.deltaTime);
    }

    //모인 뒤 턴 끝나면 공격, 힐 위치로 가는 함수
    public void Attack()
    {
        transform.Translate(enemyPos.position * Time.deltaTime);
    }

    IEnumerator ShowEffect()
    {
        while (true)
        {
            PuzzleEffect.transform.Translate(Vector3.right * Time.deltaTime);
            //Debug.Log(effect + " : " + effect.transform.position);

            yield return null;
        }
    }
}
