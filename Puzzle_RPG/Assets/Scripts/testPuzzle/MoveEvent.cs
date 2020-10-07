using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEvent
{
    public Coroutine coroutine;
    public Piece targetPiece;
    public Node targetNode;

    public Piece target { get { return targetPiece; } set { targetPiece = value; } }
    
    //public MoveEvent()
    //{
        //targetPiece = piece;
        //targetNode = destination;
    //}

//    while (true)
//        {
//            targetPiece.rectTransform.position = Vector3.Lerp(targetPiece.rectTransform.position, targetNode.pos, moveSpeed* Time.deltaTime);

//            if ((targetPiece.rectTransform.position - targetNode.pos).magnitude< 0.1f)
//            {
//                targetPiece.rectTransform.position = targetNode.pos;
//                targetPiece.originPosition = targetNode.pos;
//                break;
//            }

//yield return null;
//        }

//        //코루틴이 끝났다는 뜻
//        for (int i = 0; i<moveEventList.Count; ++i)
//        {
//            if (moveEventList[i].targetPiece == targetPiece)
//            {
//                Debug.Log(moveEventList[i].targetPiece);
//                moveEventList[i].coroutine = null;
//                break;
//            }
//        }
}
