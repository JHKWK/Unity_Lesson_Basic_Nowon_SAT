using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;
    public bool isSetAllTilseDone;

    [Header("난이도 (%)")]
    [SerializeField] int difficulty;

    // Main Camera
    [Header("오브젝트")]
    [SerializeField] Camera gamePlayCamera;
    [SerializeField] Transform background;
    // Node Prefab
    [SerializeField] Transform node;

    //파라미터    
    public int FlaggedTilesCount
    {
        get { return _flaggedTilesCount; }

        set
        {
            _flaggedTilesCount = value;
            GUICanvasManager.instance.remainMinesCountText.text = (GeneratedMinesCount- FlaggedTilesCount).ToString();

            if (CheckGameClear())
            {
                WinEvent();
            }
        }
    }
    public int GeneratedMinesCount
    {
        get { return _genratedMinesCount; }
        set
        {
            _genratedMinesCount = value;
            GUICanvasManager.instance.remainMinesCountText.text = (GeneratedMinesCount - FlaggedTilesCount).ToString();
        }
    }
    public int OpenedTilesCount
    {
        get { return _openedTilesCount; }
        set 
        {
            _openedTilesCount = value;
            GUICanvasManager.instance.openedTilesText.text = _openedTilesCount.ToString();
            if (CheckGameClear())
            {
                WinEvent();
            }
        }
    }
    public int TilesSetDone
    {
        get { return _tilesSetDone; }
        set
        {
            _tilesSetDone = value;
            if (_tilesSetDone == width*height)
                isSetAllTilseDone = true;
        }
    }

    int _tilesSetDone;
    int _flaggedTilesCount;
    int _genratedMinesCount;
    int _openedTilesCount;

    Transform tiles;
    Node[,] nodes;

    float originalCameraorthographicSize;
    float scale;
    int setMineCount;
    int width;
    int height;
    int totalTilesCount;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;

        originalCameraorthographicSize = gamePlayCamera.orthographicSize;
        tiles = new GameObject().transform;        
        tiles.parent = transform;
        tiles.name = "Tiles";
    }
    public void SetWH10x10(int a)
    {
        width = a / 100;
        height = a % 100;
        totalTilesCount = width * height;

        SetScale();
    }
    public void SetWH100x100(int a)
    {
        width = a / 1000;
        height = a % 1000;
        totalTilesCount = width * height;

        SetScale();
    }
    public bool DestroyTiles()
    {
        if (nodes != null)
            nodes = null;

        if (tiles.childCount > 0)
        {
            Node[] arrs = tiles.GetComponentsInChildren<Node>();
            foreach (var tile in arrs)
            {
                Destroy(tile.gameObject);
            }
        }
        return true;
    }
    public void GenerateTiles()
    {
        StartCoroutine(E_GenerateTilse());
    }
    IEnumerator E_GenerateTilse()
    {
        Debug.Log($"StageManager.E_GenerateTilse() 시작");
        isSetAllTilseDone = false;

        //기존 타일 초기화        
        Debug.Log($"StageManager. DestroyTiles 시작");
        yield return new WaitUntil(() => DestroyTiles());
        Debug.Log($"StageManager. DestroyTiles 완료");

        TilesSetDone = 0;
        FlaggedTilesCount = 0;
        OpenedTilesCount = 0;
        GeneratedMinesCount = 0;
        transform.localScale = Vector3.one;
        transform.position = Vector3.zero;
        tiles.transform.localPosition = Vector3.zero;

        nodes = new Node[width, height];

        ResetCamera();

        Debug.Log($"StageManager.타일 생성 시작");
        //TileNode 생성
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Transform tile = Instantiate(node, new Vector3(i, j, 0), Quaternion.identity, tiles);
                nodes[i, j] = tile.GetComponent<Node>();
            }
        }
        Debug.Log($"StageManager.타일 생성 완료");

        //지뢰 생성
        Debug.Log($"StageManager.지뢰배치 시작");
        for (int n = 0; n < setMineCount;)
        {
            int i = Random.Range(0, width);
            int j = Random.Range(0, height);
            if ( nodes[i, j].nodeType == NodeType.Num )
            {
                nodes[i, j].nodeType = NodeType.Mine;
                GeneratedMinesCount++;
                n++;
            }
        }
        Debug.Log($"StageManager.지뢰배치 완료");

        Debug.Log($"StageManager.All Node.Setup() 시작");
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                nodes[i, j].Setup();
            }
        }        
        yield return new WaitUntil(()=> isSetAllTilseDone);
        Debug.Log($"StageManager.All Node.Setup() 완료");
        Debug.Log($"StageManager.E_GenerateTilse() 종료");
        PlayManager.instance.ChangeGameStatusOnPlay();
        yield return null;
    }

    void SetScale()
    {
        // 표준 size : 32x16
        if (width >= height * 2)
        {
            if (width != 32)
                scale = 1f / (float)width * 32f;
            else scale = 1f;
        }

        else if (width < height * 2f)
        {
            if (height != 16)
                scale = 1f / (float)height * 16f;
            else scale = 1f;
        }

        setMineCount = Mathf.RoundToInt(width * height * difficulty * 0.01f);

        Debug.Log($" StageManager.SetWH 가로 : {width}, 세로 : {height}, 축적 : {scale}, 지뢰갯수설정 : {setMineCount}");
    }
    void ResetCamera()
    {        
        gamePlayCamera.transform.position = new Vector3( (width-1) * 0.5f, (height-1) * 0.5f, -10);
        gamePlayCamera.transform.position += Vector3.up * (1 / scale);
        gamePlayCamera.orthographicSize = originalCameraorthographicSize / scale;
        background.localScale = Vector3.one * 1f / scale;

    }
    bool CheckGameClear()
    {        
        if( _flaggedTilesCount +_openedTilesCount == totalTilesCount)
            return true;
        else
            return false;
    }

    //게임클리어 이벤트 추후수정
    //=========================
    //=========================
    void WinEvent()
    {
        PlayManager.instance.ChangeGameStatusWin();
    }

    public void GameOver()
    {
        PlayManager.instance.ChangeGameStatusGameOver();
    }
}