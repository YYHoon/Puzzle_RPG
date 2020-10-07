using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovePiece : MonoBehaviour
{
    static MovePiece instance;
    public static MovePiece Instance { get { return instance; } }

    Index final;  //추가할 인덱스
    Index idx;    //최종적으로 이동할 인덱스
    Piece movingPiece; //내가 움직이고 있는 피스
    
    [SerializeField] float maxDragRange = 0.5f;

    private void Awake()
    {
        instance = this;
    }
    
    public void Click(PointerEventData eventData, Piece piece)
    {
        movingPiece = piece;
        idx = new Index(movingPiece.index.x, movingPiece.index.y);
    }

    public void Drag(PointerEventData eventData, float moveSpeed = 16f)
    {
        if (movingPiece == null)
            return;

        RectTransform rectTransform = GetComponent<RectTransform>();

        Vector3 worldPoint;

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, Camera.main, out worldPoint))
        {
            Vector3 direction = worldPoint - movingPiece.originPosition;
            Vector3 normalDirection = direction.normalized;
            Vector3 absolDirection = new Vector3(Mathf.Abs(direction.x), Mathf.Abs(direction.y), 0f);

            float range = direction.magnitude;

            //피스가 이동 제한 범위를 넘으려고 할 경우
            if (maxDragRange < range)
            {
                movingPiece.rectTransform.position = movingPiece.originPosition + direction.normalized * maxDragRange;

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
                movingPiece.rectTransform.position = worldPoint;
            }
        }
    }

    public void Drop()
    {
        idx.add(final);
        GameBoard.Instance.SwapPiece(movingPiece.index, idx);

        movingPiece = null;
        idx = new Index(0, 0);
    }
}
