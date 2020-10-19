using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum PIECETYPE
{
    fire = 0,
    water = 1,
    plant = 2,
    heal = 3,
    none = 4
}

public class Piece : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] PIECETYPE pieceType;
    RectTransform rtTransform;
    Image img;
    Index idx;
    Vector3 originPos;

    public PIECETYPE piecetype { get { return pieceType; } set { pieceType = value; } }
    public RectTransform rectTransform { get { return rtTransform; } }
    public Image image { get { return img; } set { img = value; } }
    public Index index { get { return idx; } set { idx = value; } }
    public Vector3 originPosition { get { return originPos; } set { originPos = value; } }

    //public void Initialize(PIECETYPE type, Sprite sprite, Index index)
    //{
    //    img = GetComponent<Image>();
    //    rtTransform = GetComponent<RectTransform>();

    //    pieceType = type;
    //    img.sprite = sprite;
    //    //idx = index;
    //    //originPos = rtTransform.position;
    //    //Name();
    //}

    public void Create(PIECETYPE type, Sprite sprite)
    {
        img = GetComponent<Image>();
        rtTransform = GetComponent<RectTransform>();

        pieceType = type;
        img.sprite = sprite;
    }

    public void Position(Index index)
    {
        idx = index;
        originPos = rtTransform.position;
        Name();
    }

    void Name()
    {
        transform.name = "Index [" + idx.x + ", " + idx.y + "]";
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        MovePiece.Instance.Click(eventData, this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        MovePiece.Instance.Drag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        MovePiece.Instance.Drop(eventData);
    }
}