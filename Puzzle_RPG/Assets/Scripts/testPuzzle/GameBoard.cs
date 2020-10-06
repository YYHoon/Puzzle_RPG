using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameBoard : MonoBehaviour
{
    static GameBoard instance;
    public static GameBoard Instance { get { return instance; } }

    //public RectTransform gameBoard;
    GameObject piecePrefab;
    [SerializeField] Sprite[] resources;
    Node[,] nodeList;

    [SerializeField] float cellSize = 64f;
    int widthCount = 5;
    int heightCount = 6;
    //float blankWidth;
    //float blankHeight;
    //float startX;
    //float startY;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //gameBoard = GetComponent<RectTransform>();
        SetBoard();
        MixBoard();       
    }
    
    void Update()
    {
        //UpdateGravity();
    }

    //시작할 때 보드 세팅
    void SetBoard()
    {
        piecePrefab = Resources.Load("Prefabs/Puzzle/Piece") as GameObject;
        RectTransform gameBoard = GetComponent<RectTransform>();

        //widthCount = (int)(gameBoard.sizeDelta.x / cellSize);
        //heightCount = (int)(gameBoard.sizeDelta.y / cellSize);
        //blankWidth = (gameBoard.sizeDelta.x % cellSize) * 0.5f;
        //blankHeight = (gameBoard.sizeDelta.y % cellSize) * 0.5f;
        //startX = -gameBoard.sizeDelta.x * 0.5f + blankWidth + cellSize * 0.5f;
        //startY = gameBoard.sizeDelta.y * 0.5f - blankHeight - cellSize * 0.5f;

        nodeList = new Node[heightCount, widthCount];

        //보드에 피스 놓기
        for (int y = 0; y < heightCount; ++y)
        {
            for (int x = 0; x < widthCount; ++x)
            {
                //Vector2(startX + CellSize * x, startY - CellSize * y)
                Vector2 position = new Vector2(32 + cellSize * x, -32 - cellSize * y);
                Piece newPiece = RandomPiece(new Index(x, y), position);
                nodeList[y, x] = new Node(x, y, newPiece.rectTransform.position, newPiece);
            }
        }
    }

    //중복 피스 재설정
    void MixBoard()
    {
        List<PIECETYPE> equal = new List<PIECETYPE>();

        for (int x = 0; x < widthCount; x++)
        {
            for (int y = 0; y < heightCount; y++)
            {
                Index idx = new Index(x, y);
                PIECETYPE type = GetPieceType(idx);

                List<Node> matchList = CheckMatch(idx);

                while (matchList.Count > 0)
                {
                    //mathList[mathList.Count / 2] = 
                    
                    if (!equal.Contains(type))
                        equal.Add(type);
                    
                    PIECETYPE newType = ResetPieceType(equal);
                    
                    nodeList[y, x].setPieceType(newType);
                    GetPiece(idx).image.sprite = resources[(int)newType];

                    matchList = CheckMatch(idx);
                }
                equal.Clear();
            }
        }
    }

    //중복 피스 중 겹치지 않는 타입 리턴
    PIECETYPE ResetPieceType(List<PIECETYPE> equal)
    {
        List<PIECETYPE> type = new List<PIECETYPE>();

        //리소스 타입 다 담고
        for (int i = 0; i < resources.Length; i++)
            type.Add((PIECETYPE)i);

        //들어온 리스트에서 중복 리소스 삭제
        for (int i = 0; i < equal.Count; i++)
            type.Remove((PIECETYPE)i);

        //중복되지 않는 리소스 리턴
        if (type == null) return PIECETYPE.none;
        return type[Random.Range(0, type.Count)];
    }

    //피스 섞기
    Piece RandomPiece(Index idx, Vector2 vec)
    {
        GameObject newObj = Instantiate(piecePrefab, transform);
        Piece piece = newObj.GetComponent<Piece>();

        int random = Random.Range(0, resources.Length);
        PIECETYPE type = (PIECETYPE)random;

        piece.GetComponent<RectTransform>().anchoredPosition = vec;
        piece.Initialize(type, resources[random], idx);

        return piece;
    }

    //피스 위치 바꾸기
    public void SwapPiece(Index one, Index two)
    {
        //노드 바깥이라면
        if (GetNode(two) == null)
        {
            GetNode(one).setPiece(GetPiece(one));
            return;
        }

        Node nodeOne = GetNode(one);
        Piece pieceOne = GetPiece(one);

        Node nodeTwo = GetNode(two);
        Piece pieceTwo = GetPiece(two);

        nodeOne.setPiece(pieceTwo);
        nodeTwo.setPiece(pieceOne);

        List<Node> matchOne = CheckMatch(one);
        List<Node> matchTwo = CheckMatch(two);

        List<Node> matchList = new List<Node>();

        AddMatch(matchList, matchOne);
        AddMatch(matchList, matchTwo);

        //매치된 게 없다면
        if (matchList.Count <= 0)
        {
            nodeOne.setPiece(pieceOne);
            nodeTwo.setPiece(pieceTwo);
            return;
        }

        else
        {
            foreach (Node match in matchList)
            {
                RemovePiece(match);
            }
        }
        
        UpdateGravity();
    }

    //매치된 상태인지 확인
    List<Node> CheckMatch(Index start)
    {
        PIECETYPE startType = GetPieceType(start);
        if (startType == PIECETYPE.none)
            return null;

        List<Node> matchList = new List<Node>();

        Index[] match = {Index.right, Index.left, Index.up, Index.down };

        //한 방향으로의 일렬검사
        foreach (Index i in match)
        {
            List<Node> line = new List<Node>();
            int idxCount = i.x != 0 ? widthCount : heightCount;
            int matchCount = 0;

            for (int j = 1; j < idxCount; j++)
            {
                Index nextIndex = Index.Add(start, Index.Mult(i, j));

                if (GetPieceType(nextIndex) == startType)
                {
                    Node node = GetNode(nextIndex);
                    line.Add(node);
                    matchCount++;
                }

                else
                    break;
            }
            //2개 이상 매치면 매치리스트에 넣기
            if (matchCount >= 2)
            {
                AddMatch(matchList, line);
            }
        }

        //매치 상태인데 우리가 가운데 있는 상황일 때
        for (int i = 0; i < 4; i += 2)
        {
            Index[] direction = { match[i], match[i + 1] };
            List<Node> line = new List<Node>();
            int matchCount = 0;

            foreach (Index j in direction)
            {
                Index idx = Index.Add(start, j);
                Node node = GetNode(idx);

                if (node != null && node.piece != null &&
                    node.piece.piecetype == startType)
                {
                    line.Add(node);
                    matchCount++;
                }
            }

            if (matchCount >= 2)
            {
                AddMatch(matchList, line);
            }
        }

        if (matchList.Count != 0)
        {
            Node node = GetNode(start);
            if (!matchList.Contains(node))
                matchList.Add(GetNode(start));
        }

        return matchList;
    }

    //매치 결과에 검수 결과 중복 안 되게 추가하기
    void AddMatch(List<Node> matchList, List<Node> line)
    {
        for (int i = 0; i < line.Count; i++)
        {
            if (matchList.Contains(line[i])) continue;

            matchList.Add(line[i]);
        }
    }

    //퍼즐 이동 후
    void UpdateGravity()
    {
        for (int x = 0; x < widthCount; x++)
        {
            for (int y = heightCount - 1; y >= 0; y--)
            {
                Index current = new Index(x, y);
                Index upper = Index.Add(current, Index.up);

                if (nodeList[y, x].piece == null)
                {
                    //빈 노드 위의 인덱스를 검사
                    while (GetPiece(upper) == null)
                    {
                        upper.add(Index.up);
                        
                        //노드 벗어나면 탐색 종료
                        if (GetNode(upper) == null) break;
                    }
                    
                    //빈 노드의 위를 탐색했을 때 피스가 있다면
                    if (GetNode(upper) != null)
                    {
                        //MovePiece.Instance.Move(GetPiece(upper), GetNode(current));
                        StartCoroutine(MovePiece.Instance.Gravity(GetPiece(upper), GetNode(current)));

                        GetNode(current).piece = GetPiece(upper);
                        GetPiece(current).index = GetNode(current).index;
                        GetNode(upper).piece = null;
                    }

                    //노드 바깥이면
                    else
                    {
                        Vector2 position = new Vector2();
                        position.x = 32 + (cellSize * x);
                        position.y = -32 + (cellSize * y);
                        
                        Piece newPiece = RandomPiece(current, position);

                        //MovePiece.Instance.Move(newPiece, GetNode(current));
                        StartCoroutine(MovePiece.Instance.Gravity(newPiece, GetNode(current)));

                        GetNode(current).piece = newPiece;
                        GetPiece(current).index = GetNode(current).index;
                    }
                }
            }
        }        
    }

    //매치된 퍼즐 삭제
    void RemovePiece(Node node)
    {
        Destroy(node.piece.gameObject);
        //Destroy(node.piece);
        node.piece = null;
    }
    
    //인덱스의 노드값 받기
    Node GetNode(Index idx)
    {
        if (idx.x < 0 || idx.x >= widthCount || idx.y < 0 || idx.y >= heightCount)
            return null;
        return nodeList[idx.y, idx.x];
    }

    //인덱스의 피스값 받기
    Piece GetPiece(Index idx)
    {
        Node node = GetNode(idx);

        if (node == null)
            return null;

        return node.piece;
    }

    //인덱스의 피스타입값 받기
    PIECETYPE GetPieceType(Index idx)
    {
        Piece piece = GetPiece(idx);
        if (piece == null)
            return PIECETYPE.none;
        return piece.piecetype;
    }
}

public class Node
{
    public Index index;
    public Vector3 pos;
    public Piece piece;

    public Node(int indexX, int indexY, Vector3 position, Piece piece)
    {
        index = new Index(indexX, indexY);
        pos = position;
        this.piece = piece;
    }

    public void setPiece(Piece p)
    {
        piece = p;
        piece.SetIndex(index);
        piece.rectTransform.position = pos;
        piece.originPosition = pos;
    }

    public void setPieceType(PIECETYPE type)
    {
        piece.piecetype = type;
    }
}