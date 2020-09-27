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

    public PIECETYPE piecetype { get { return pieceType; } }
    public RectTransform rectTransform { get { return rtTransform; } }
    public Index index { get { return idx; } set { idx = value; } }

    public void Initialize(PIECETYPE type, Sprite sprite, Index index)
    {
        img = GetComponent<Image>();
        rtTransform = GetComponent<RectTransform>();

        pieceType = type;
        img.sprite = sprite;
        idx = index;
        Name();
    }

    public void SetType(PIECETYPE type)
    {
        this.pieceType = type;
    }

    void Name()
    {
        transform.name = "Index [" + idx.x + ", " + idx.y + "]";
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
