using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    // 지뢰 생성 빈도 = 1/randomSeed
    [SerializeField] int randomSeed;
    // Node Prefab
    [SerializeField] Transform node;
    //node 배치할 부모 GameObject
    [SerializeField] Transform tiles;

    //OnPlay패널
    [SerializeField] Text FlaggedCountText;
    [SerializeField] Text OpenedTileText;
    [SerializeField] Text WinText;

    public float scale ;
    public int FlaggedTilesCount
    {
        get { return _FlaggedTileCount; }

        set
        {
            _FlaggedTileCount = value;
            FlaggedCountText.text = _FlaggedTileCount.ToString();
            if (CheckGameClear())
            {
                WinEvent();
            }
        }
    }
    public int MinesCout
    {
        get { return _MineCount; }

        set
        {
            _MineCount = value;
        }
    }
    public int OpenedTileCount
    {
        get { return _OpenedTileCount; }
        set 
        {
            _OpenedTileCount = value;
            OpenedTileText.text = _OpenedTileCount.ToString();
            if (CheckGameClear())
            {
                WinEvent();
            }
        }
    }

    int _FlaggedTileCount = 0;
    int _MineCount = 0;
    int _OpenedTileCount=0;
    
    Node[,] nodes;

    int width;
    int height;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;

    }

    public void SetWH(int w)
    {
        width = w / 100;
        height = w % 100;

        // max size : 28x14
        if (width >= height * 2)
        {
            if (width != 28)
                scale = 1f / (float)width * 28f;
            else scale = 1f;
        }

        else if (width < height * 2f)
        {
            if (height != 14)
                scale = 1 / (float)height * 14f;
            else scale = 1f;
        }

        Debug.Log("가로세로 Setup");
        Debug.Log($"가로 : {width}, 세로 : {height}, 축적 : {scale}");
    }
    public void GenerateTiles()
    {
        StartCoroutine(E_GenerateTilse());
    }
    IEnumerator E_GenerateTilse()
    {
        FlaggedTilesCount = 0;
        OpenedTileCount = 0;
        WinText.enabled = false;

        transform.localScale = Vector3.one;
        transform.position = Vector3.zero;
        tiles.transform.localPosition = Vector3.zero;

        //TileNode 초기화
        DestroyTiles();                
        yield return new WaitUntil(() => tiles.GetComponentInChildren<Node>() == null);
        Debug.Log($"tiles Child Count : {tiles.childCount}");

        nodes = new Node[width, height];

        //TileNode 생성
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Transform tile = Instantiate(node, new Vector3(i, j, 0), Quaternion.identity, tiles);
                nodes[i, j] = tile.GetComponent<Node>();

                int a = Random.Range(0, randomSeed);
                if (a == 0)
                {
                    nodes[i, j].nodeType = NodeType.Mine;
                    MinesCout++;
                }
                    
                else
                    nodes[i, j].nodeType = NodeType.Num;
            }
        }
        Debug.Log($"Node생성 완료");

        //TileNode 셋업
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Debug.Log(nodes[i, j].nodeType);
                nodes[i, j].Setup();
            }
        }
        Debug.Log($"Node셋업 완료");

        //TileNode 리사이즈&리포지션
        ResetPosCenter();

        yield return null;
    }
    public void DestroyTiles()
    {
        if (tiles.childCount > 0)
        {
            Node[] arrs = tiles.GetComponentsInChildren<Node>();
            foreach (var tile in arrs)
            {
                Destroy(tile.gameObject);
            }
        }
    }
    void ResetPosCenter()
    {
        tiles.position = new Vector3(0.5f - (float)width * 0.5f, 0.5f - (float)height * 0.5f, 0);
        transform.position = Vector3.up * -(16 - height*scale) * 0.4f;
        transform.localScale = Vector3.one * scale;
        Debug.Log($"리스케일 리포지션 완료");
    }
    bool CheckGameClear()
    {
        int allTileCount = width * height;
        if( _FlaggedTileCount +_OpenedTileCount == allTileCount)
            return true;
        else
            return false;
    }

    void WinEvent()
    {
        WinText.enabled = true;
    }
}
