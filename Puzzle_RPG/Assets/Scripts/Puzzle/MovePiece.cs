using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovePiece : MonoBehaviour
{
    static MovePiece instance;
    public static MovePiece Instance { get { return instance; } }

    Index final;                  //추가할 인덱스
    Index idx;                    //최종적으로 이동할 인덱스
    Piece movingPiece;            //내가 움직이고 있는 피스
    float maxDragRange = 0.5f;    //피스 이동 제한거리
    float distance;               //마우스와 피스 거리
    bool moving = false;          //턴제를 위해서 마우스를 UP할 때 활성화
    
    public bool Moving { get { return moving; } set { moving = value; } }

    private void Awake()
    {
        instance = this;
    }
    
    public void Click(PointerEventData eventData, Piece piece)
    {
        if (!GameBoard.Instance.IsMoveEventEnd() || moving == true)
            return;

        movingPiece = piece;
        idx = new Index(movingPiece.index.x, movingPiece.index.y);
    }

    public void Drag(PointerEventData eventData, float moveSpeed = 16f)
    {
        if (movingPiece == null || !GameBoard.Instance.IsMoveEventEnd() || moving == true)
            return;

        RectTransform rectTransform = GetComponent<RectTransform>();

        Vector3 worldPoint;

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, Camera.main, out worldPoint))
        {
            Vector3 direction = worldPoint - movingPiece.originPosition;
            Vector3 normalDirection = direction.normalized;
            Vector3 absolDirection = new Vector3(Mathf.Abs(direction.x), Mathf.Abs(direction.y), 0f);

            float range = direction.magnitude;
            distance = Vector3.Distance(worldPoint, movingPiece.originPosition);

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

    public void Drop(PointerEventData eventData)
    {
        if (!GameBoard.Instance.IsMoveEventEnd() || moving == true)
            return;

        //피스의 본래 포지션과 마우스 좌표의 차이가 0.15f 미만이면
        //본래 포지션으로 돌려놓기
        if (distance < 0.15f)
        {
            movingPiece.rectTransform.position = movingPiece.originPosition;
            return; 
        }

        //아니면 스왑하기
        else
        {
            idx.add(final);
            GameBoard.Instance.SwapPiece(movingPiece.index, idx);
        }

        moving = true;
        distance = 0f;
        movingPiece = null;
        idx = new Index(0, 0);
    }
}
