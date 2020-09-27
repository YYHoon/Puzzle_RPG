using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovePiece
{
    public static MovePiece Instance;
    GameBoard board;
    
    Index idx;
    Piece target;
    Vector2 startPos;
    float moveSpeed;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void move(Piece piece)
    {
        Debug.Log("move");
    }

    public void OnDrag(PointerEventData eneventData)
    {

    }

    public void drop()
    {
        Debug.Log("drop");
    }
}
