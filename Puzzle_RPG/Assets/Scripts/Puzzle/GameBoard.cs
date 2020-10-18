using System.Collections;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveEvent
{
    public Coroutine coroutine;
    public Piece targetPiece;
}

public struct Attack
{
    public float fire;
    public float water;
    public float plant;
    public float heal;
}

public class GameBoard : MonoBehaviour
{
    static GameBoard instance;
    public static GameBoard Instance { get { return instance; } }

    [Header("공격")]
    Attack attack;
    List<Piece> attackList = new List<Piece>();

    float count = 0;        //턴 지나는지 확인할 카운트
    bool turn = false;      //다시 턴을 진행할 수 있을 경우 (true)

    public bool PlayerTurn { get { return turn; } set { turn = value; } }

    [Header("피스에 필요한 것들")]
    List<Piece> pieceList = new List<Piece>();
    int pieceCount = 100;
    GameObject piecePrefab;
    [SerializeField] Sprite[] resources;
    Node[,] nodeList;

    [Header("다 움직인 피스들")]
    List<MoveEvent> moveEventList = new List<MoveEvent>();
    [SerializeField] GameObject[] effects = new GameObject[4];

    [Header("게임보드 칸")]
    [SerializeField] float cellSize = 64f;
    int widthCount = 7;
    int heightCount = 8;
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
        PiecePool();
        SetBoard();
        MixBoard();
    }

    void Update()
    {
        //피스의 움직임이 끝났으면
        if (IsMoveEventEnd())
        {
            List<Node> matchList = new List<Node>();

            for (int i = 0; i < moveEventList.Count; ++i)
            {
                AddMatch(matchList, CheckMatch(moveEventList[i].targetPiece.index));
            }

            if (matchList.Count != 0)
            {
                for (int i = 0; i < matchList.Count; i++)
                {
                    attackList.Add(matchList[i].piece);
                    RemovePiece(matchList[i]);

                }
            }

            moveEventList.Clear();
            PlayerAttack();
            UpdateGravity();
        }

        //마우스 드랍 상태면
        if (MovePiece.Instance.Moving)
        {
            count += Time.deltaTime;

            //시간 1초 지나면 턴 활성화
            if (count >= 0.8f)
            {
                count = 0;
                turn = true;
                MovePiece.Instance.Moving = false;
            }
        }

        //피스가 움직이고 있는 동안에는 카운트 초기화
        if (!IsMoveEventEnd())
        {
            count = 0;
            turn = false;
        }
    }

    //피스 미리 만들기
    void PiecePool()
    {
        piecePrefab = Resources.Load("Prefabs/Puzzle/Piece") as GameObject;

        for (int i = 0; i < pieceCount; i++)
        {
            Piece piece = CreatePiece();
            piece.gameObject.SetActive(false);
            pieceList.Add(piece);
        }
    }

    //시작할 때 보드 세팅
    void SetBoard()
    {
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
                int random = Random.Range(0, pieceList.Count);
                //Vector2(startX + CellSize * x, startY - CellSize * y)
                Vector2 position = new Vector2(32 + cellSize * x, -32 - cellSize * y);

                //피스 새로 생성
                //Piece newPiece = RandomPiece(new Index(x, y), position);

                //피스리스트에서 꺼내서 자리 정해주기
                Piece newPiece = pieceList[random];
                PiecePosition(newPiece, new Index(x, y), position);
                newPiece.gameObject.SetActive(true);
                pieceList.RemoveAt(random);

                nodeList[y, x] = new Node(x, y, newPiece.rectTransform.position, newPiece);
            }
        }
    }

    //처음 세팅 시 중복 피스 재설정
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
                    if (!equal.Contains(type))
                        equal.Add(type);

                    PIECETYPE newType = ResetPieceType(equal);

                    //매치리스트 중간 인덱스 노드에 있는 피스 타입 변경
                    Node node = GetNode(matchList[matchList.Count / 2].index);
                    node.setPieceType(newType);
                    GetPiece(node.index).image.sprite = resources[(int)newType];

                    //nodeList[y, x].setPieceType(newType);
                    //GetPiece(idx).image.sprite = resources[(int)newType];

                    matchList = CheckMatch(idx);
                }
                equal.Clear();
            }
        }
    }

    //피스 자신과 겹치지 않는 타입 리턴
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

    //피스 생성
    Piece CreatePiece()
    {
        GameObject newObj = Instantiate(piecePrefab, transform);
        Piece piece = newObj.GetComponent<Piece>();

        int random = Random.Range(0, resources.Length);
        PIECETYPE type = (PIECETYPE)random;

        piece.Create(type, resources[random]);

        return piece;
    }

    //피스 자리
    void PiecePosition(Piece piece, Index idx, Vector2 vec)
    {
        piece.GetComponent<RectTransform>().anchoredPosition = vec;
        piece.Position(idx);
    }

    //피스 랜덤하게 생성(피스 리스트 전)
    //Piece RandomPiece(Index idx, Vector2 vec)
    //{
    //    GameObject newObj = Instantiate(piecePrefab, transform);
    //    Piece piece = newObj.GetComponent<Piece>();

    //    int random = Random.Range(0, resources.Length);
    //    PIECETYPE type = (PIECETYPE)random;

    //    piece.GetComponent<RectTransform>().anchoredPosition = vec;
    //    piece.Initialize(type, resources[random], idx);

    //    return piece;
    //}

    //피스 위치 바꾸기
    public void SwapPiece(Index one, Index two)
    {
        //노드 바깥이라면
        if (GetNode(two) == null)
        {
            MoveEvent Return = new MoveEvent();
            Return.targetPiece = GetPiece(one);
            Return.coroutine = StartCoroutine(Move(GetPiece(one), GetNode(one)));
            moveEventList.Add(Return);
            return;
        }

        Node nodeOne = GetNode(one);
        Piece pieceOne = GetPiece(one);

        Node nodeTwo = GetNode(two);
        Piece pieceTwo = GetPiece(two);

        //==========================인덱스 값만 먼저 바꿔보기==========================//
        //피스원 인덱스 설정
        nodeTwo.setIndex(pieceOne);

        //피스투 인덱스 설정
        nodeOne.setIndex(pieceTwo);

        //======================자리 교체 전 매치 확인======================//
        List<Node> matchList = new List<Node>();
        List<Node> matchOne = CheckMatch(one);
        List<Node> matchTwo = CheckMatch(two);

        AddMatch(matchList, matchOne);
        AddMatch(matchList, matchTwo);

        //매치된 퍼즐이 없으면 제자리로 돌려놓기
        if (matchList.Count == 0)
        {
            //MoveEvent pieceOneBackEvent = new MoveEvent();
            //pieceOneBackEvent.targetPiece = pieceOne;
            //pieceOneBackEvent.coroutine = StartCoroutine(Move(pieceOne, nodeOne));
            //moveEventList.Add(pieceOneBackEvent);
            //nodeOne.setIndex(pieceOne);

            //MoveEvent pieceTwoBackEvent = new MoveEvent();
            //pieceTwoBackEvent.targetPiece = pieceTwo;
            //pieceTwoBackEvent.coroutine = StartCoroutine(Move(pieceTwo, nodeTwo));
            //moveEventList.Add(pieceTwoBackEvent);
            //nodeTwo.setIndex(pieceTwo);

            StartCoroutine(Move(pieceOne, nodeOne));
            nodeOne.setIndex(pieceOne);

            StartCoroutine(Move(pieceTwo, nodeTwo));
            nodeTwo.setIndex(pieceTwo);
        }

        //매치된 퍼즐이 있으면
        else
        {
            //피스원부터 코루틴
            MoveEvent pieceOneMoveEvent = new MoveEvent();
            pieceOneMoveEvent.targetPiece = pieceOne;
            pieceOneMoveEvent.coroutine = StartCoroutine(Move(pieceOne, nodeTwo));

            //피스투 코루틴
            MoveEvent pieceTwoMoveEvent = new MoveEvent();
            pieceTwoMoveEvent.targetPiece = pieceTwo;
            pieceTwoMoveEvent.coroutine = StartCoroutine(Move(pieceTwo, nodeOne));

            moveEventList.Add(pieceOneMoveEvent);
            moveEventList.Add(pieceTwoMoveEvent);
        }
    }

    //매치된 상태인지 확인
    List<Node> CheckMatch(Index start)
    {
        PIECETYPE startType = GetPieceType(start);
        if (startType == PIECETYPE.none)
            return null;

        List<Node> matchList = new List<Node>();

        Index[] match = { Index.right, Index.left, Index.up, Index.down };

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

    //매치 되었을 경우 에너미를 공격
    void PlayerAttack()
    {
        if (attackList.Count == 0) return;

        //매치된 피스의 속성 값별로 구조체에 더해서
        for (int i = 0; i < attackList.Count; i++)
        {
            if (attackList[i].piecetype == PIECETYPE.fire)
                attack.fire += 1;

            else if (attackList[i].piecetype == PIECETYPE.water)
                attack.water += 1;

            else if (attackList[i].piecetype == PIECETYPE.plant)
                attack.plant += 1;

            else if (attackList[i].piecetype == PIECETYPE.heal)
                attack.heal += 1;
        }

        //에너미 힛 함수로 넘기기
        EnemySpawn.Instance.EnemyHit(attack);

        //초기화
        {
            attack.fire = 0;
            attack.water = 0;
            attack.plant = 0;
            attack.heal = 0;
            attackList.Clear();
        }
    }

    //퍼즐 이동 후
    public void UpdateGravity()
    {
        for (int x = 0; x < widthCount; x++)
        {
            //거꾸로 올라가면서 탐색
            for (int y = heightCount - 1; y >= 0; y--)
            {
                Index current = new Index(x, y);
                Index upper = Index.Add(current, Index.up);

                //노드의 피스가 비어있으면
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
                        MoveEvent newEvent = new MoveEvent();
                        newEvent.targetPiece = GetPiece(upper);
                        newEvent.coroutine = StartCoroutine(Move(GetPiece(upper), GetNode(current)));
                        moveEventList.Add(newEvent);

                        GetNode(current).piece = GetPiece(upper);
                        GetPiece(current).index = GetNode(current).index;
                        //movingPiece.Add(GetPiece(current));
                        GetNode(upper).piece = null;
                    }

                    //노드 바깥까지 갔는데 피스가 없으면
                    else
                    {
                        Vector2 position = new Vector2();
                        position.x = 32 + (cellSize * x);
                        position.y = (-32 + (cellSize * y)) + 512;

                        //피스 리스트에서 꺼내 쓰기
                        if (pieceList.Count != 0)
                        {
                            int random = Random.Range(0, pieceList.Count);

                            Piece oldPiece = pieceList[random];
                            oldPiece.gameObject.SetActive(true);
                            pieceList.RemoveAt(random);
                            
                            oldPiece.rectTransform.position = position;

                            MoveEvent newEvent = new MoveEvent();
                            newEvent.targetPiece = oldPiece;
                            newEvent.coroutine = StartCoroutine(Move(oldPiece, GetNode(current)));
                            moveEventList.Add(newEvent);

                            GetNode(current).piece = oldPiece;
                            GetPiece(current).index = GetNode(current).index;
                        }

                        //피스 리스트가 없었을 때
                        //else
                        //{
                        //    //피스 새로 생성
                        //    Piece newPiece = RandomPiece(current, position);
                        //
                        //    MoveEvent newEvent = new MoveEvent();
                        //    newEvent.targetPiece = newPiece;
                        //    newEvent.coroutine = StartCoroutine(Move(newPiece, GetNode(current)));
                        //    moveEventList.Add(newEvent);
                        //
                        //    GetNode(current).piece = newPiece;
                        //    GetPiece(current).index = GetNode(current).index;
                        //}
                    }
                }
            }
        }
    }

    //움직이는 피스가 있는지 확인하는 bool
    public bool IsMoveEventEnd()
    {
        for (int i = 0; i < moveEventList.Count; ++i)
        {
            if (moveEventList[i].coroutine != null)
                return false;
        }

        return true;
    }

    //퍼즐 움직이는 코루틴
    public IEnumerator Move(Piece piece, Node destination, float moveSpeed = 9.0f)
    {
        while (true)
        {
            piece.rectTransform.position = Vector3.Lerp(piece.rectTransform.position, destination.pos, moveSpeed * Time.deltaTime);

            if ((piece.rectTransform.position - destination.pos).magnitude < 0.1f)
            {
                piece.rectTransform.position = destination.pos;
                piece.originPosition = destination.pos;
                break;
            }

            yield return null;
        }

        //코루틴이 끝났다는 뜼
        for (int i = 0; i < moveEventList.Count; ++i)
        {
            if (moveEventList[i].targetPiece == piece)
            {
                moveEventList[i].coroutine = null;
                break;
            }
        }
    }

    //매치된 퍼즐 삭제
    void RemovePiece(Node node)
    {
        //int index = (int)GetPieceType(node.index);
        //RectTransform start = GetPiece(node.index).rectTransform;
        //
        //EffectManager.Instance.CreateEffect(index, start);

        //=========================================//
        //Destroy(node.piece.gameObject);
        node.piece.gameObject.SetActive(false);
        pieceList.Add(node.piece);
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

    public void setIndex(Piece p)
    {
        piece = p;
        piece.index = this.index;
        //piece.rectTransform.position = pos;
        //piece.originPosition = pos;
    }

    public void setPieceType(PIECETYPE type)
    {
        piece.piecetype = type;
    }
}