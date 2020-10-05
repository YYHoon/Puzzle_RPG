using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovePiece : MonoBehaviour
{
    static MovePiece instance;
    public static MovePiece Instance { get { return instance; } }
    Index final;

    Index idx;
    Piece moving;

    float moveSpeed = 16f;

    [SerializeField] float maxDragRange = 0.5f;

    private void Awake()
    {
        instance = this;
    }
    
    public void Click(PointerEventData eventData, Piece piece)
    {
        moving = piece;
        idx = new Index(moving.index.x, moving.index.y);
    }

    public void Drag(PointerEventData eventData)
    {
        if (moving == null)
            return;

        RectTransform rectTransform = GetComponent<RectTransform>();

        Vector3 worldPoint;

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, Camera.main, out worldPoint))
        {
            Vector3 direction = worldPoint - moving.originPosition;
            Vector3 normalDirection = direction.normalized;
            Vector3 absolDirection = new Vector3(Mathf.Abs(direction.x), Mathf.Abs(direction.y), 0f);

            float range = direction.magnitude;

            //피스가 이동 제한 범위를 넘으려고 할 경우
            if (maxDragRange < range)
            {
                moving.rectTransform.position = moving.originPosition + direction.normalized * maxDragRange;

                //좌우
                if (absolDirection.x > absolDirection.y)
                {
                    if (normalDirection.x > 0) final = new Index(1, 0);
                    else final = new Index(-1, 0);
                }

                //상하
                else
                {
                    if (normalDirection.y > 0) final = new Index(0, -1);
                    else final = new Index(0, 1);
                }
            }
            else
            {
                moving.rectTransform.position = worldPoint;
            }
        }
    }

    public void Drop()
    {
        idx.add(final);
        GameBoard.Instance.SwapPiece(moving.index, idx);

        moving = null;
        idx = new Index(0, 0);
        //Debug.Log("up : " + idx.x + ", " + idx.y);
    }
}
