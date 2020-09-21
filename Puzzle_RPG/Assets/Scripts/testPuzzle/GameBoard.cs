using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    //static GameBoard sInstance;
    //public static GameBoard Instance { get { return sInstance; } }

    //public RectTransform gameBoard;
    GameObject piecePrefab;
    [SerializeField] Sprite[] resources;
    Node[,] nodeList;

    [SerializeField] float cellSize = 64f;
    int widthCount = 5;
    int heightCount = 6;
    float blankWidth;
    float blankHeight;
    float startX;
    float startY;

    //private void Awake()
    //{
    //    sInstance = this;
    //}

    void Start()
    {
        //gameBoard = GetComponent<RectTransform>();
        SetBoard();
    }
    
    void Update()
    {
        
    }

    void SetBoard()
    {
        piecePrefab = Resources.Load("Prefabs/Piece") as GameObject;
        RectTransform gameBoard = GetComponent<RectTransform>();

        //widthCount = (int)(gameBoard.sizeDelta.x / cellSize);
        //heightCount = (int)(gameBoard.sizeDelta.y / cellSize);
        //blankWidth = (gameBoard.sizeDelta.x % cellSize) * 0.5f;
        //blankHeight = (gameBoard.sizeDelta.y % cellSize) * 0.5f;
        //startX = -gameBoard.sizeDelta.x * 0.5f + blankWidth + cellSize * 0.5f;
        //startY = gameBoard.sizeDelta.y * 0.5f - blankHeight - cellSize * 0.5f;

        nodeList = new Node[heightCount, widthCount];

        for (int y = 0; y < heightCount; ++y)
        {
            for (int x = 0; x < widthCount; ++x)
            {
                //Vector2(startX + CellSize * x, startY - CellSize * y)
                Vector2 position = new Vector2(32 + cellSize * x, -32 - cellSize * y);
                Piece newPiece = RandomPiece(new Index(x, y), position);
                nodeList[y, x] = new Node(x, y, newPiece.rectTransform.anchoredPosition, newPiece);
            }
        }
    }

    Piece RandomPiece(Index idx, Vector2 vec)
    {
        GameObject newObj = Instantiate(piecePrefab, transform);
        Piece piece = newObj.GetComponent<Piece>();

        int random = Random.Range(0, resources.Length);
        PIECETYPE type = (PIECETYPE)random;

        piece.Initialize(type, resources[random], idx);
        piece.rectTransform.anchoredPosition = vec;

        return piece;
    }
}

public class Node
{
    public Index idx;
    public Vector2 pos;
    public Piece piece;

    public Node(int indexX, int indexY, Vector2 position, Piece piece)
    {
        idx = new Index(indexX, indexY);
        pos = position;
        this.piece = piece;
    }
}