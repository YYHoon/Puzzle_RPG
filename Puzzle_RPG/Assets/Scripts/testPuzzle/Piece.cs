using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PIECETYPE
{
    fire = 0,
    water = 1,
    plant = 2,
    heal = 3,
    none = 4
}

public class Piece : MonoBehaviour
{
    [SerializeField] PIECETYPE pieceType;
    RectTransform rtTransform;
    Image img;
    Index idx;

    public PIECETYPE piecetype { get { return pieceType; } set { pieceType = piecetype; } }
    public RectTransform rectTransform { get { return rtTransform; } }
    public Index index { get { return idx; } set { idx = value; } }

    public void Initialize(PIECETYPE type, Sprite sprite, Index index)
    {
        img = GetComponent<Image>();
        rtTransform = GetComponent<RectTransform>();

        pieceType = type;
        img.sprite = sprite;
        idx = index;
    }
}
