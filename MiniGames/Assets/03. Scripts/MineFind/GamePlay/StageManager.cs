using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

/// <summary>
/// 구현할것
/// 
///  Allclear 판단
/// 
/// 지뢰 밟았을때 이벤트
/// 
/// </summary>

public enum StageStatus{ FirstStart, Retry, NextStage }
public class StageManager : MonoBehaviour
{
    public static StageManager instance;
    public bool isSetAllTilseDone;
    public StageStatus status;

    [Header("설정")]
    [SerializeField] int startHeart;
    [SerializeField] int maxHeart;
    
    // Main Camera
    [Header("오브젝트")]
    [SerializeField] Camera gamePlayCamera;
    [SerializeField] Transform background;    
    [SerializeField] Transform node;

    // Stag
    [Header("스테이지 에셋")]
    [SerializeField] List <StageInfo> stageInfos;
    [SerializeField] List <SkinInfo> skinInfos;
    SkinInfo _skinInfo;

    //프로퍼티
    public int Heart
    {
        get { return _heart; }
        set
        {
            _heart = value;
            if (_heart > maxHeart)
            {
                Heart = maxHeart;
                TotalScore += 1000;
            }
            GUICanvasManager.instance.UpdateHearts(_heart);

            if (_heart == 0)            
                GameOver();            
        }
    }
    public int FlaggedTilesCount
    {
        get { return _flaggedTilesCount; }

        set
        {
            _flaggedTilesCount = value;
            GUICanvasManager.instance.remainMinesCountText.text = (GeneratedMinesCount - FlaggedTilesCount - OpenedMinesCount).ToString();

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
            GUICanvasManager.instance.remainMinesCountText.text = GeneratedMinesCount.ToString();
        }
    }
    public int OpenedNumsCount
    {
        get { return _openedNumsCount; }
        set 
        {
            _openedNumsCount = value;

            TotalScore += score;

            GUICanvasManager.instance.openedTilesText.text = (_openedNumsCount+_opendeMinesCount).ToString();
            if (CheckGameClear())
            {
                WinEvent();
            }
        }
    }
    public int OpenedMinesCount
    {
        get { return _opendeMinesCount; }
        set
        {
            _opendeMinesCount = value;

            TotalScore -= score;

            GUICanvasManager.instance.openedTilesText.text = (_openedNumsCount + _opendeMinesCount).ToString();
            GUICanvasManager.instance.remainMinesCountText.text = (GeneratedMinesCount - FlaggedTilesCount - OpenedMinesCount).ToString();
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
            if ( width*height != 0  &&  _tilesSetDone == width*height )
                isSetAllTilseDone = true;
        }
    }
    public int CurrentStage
    {
        get { return _currentStage; }
        set 
        {
            _currentStage = value; 
            if (CurrentStage+1 > stageInfos.Count)
                CurrentStage = stageInfos.Count-1;
        }
    }
    public int TotalScore
    {
        get { return _totalScore; }
        set 
        {
            _totalScore = value; 
            GUICanvasManager.instance.TotalScore.text = _totalScore.ToString();
        }
    }
    public int ClearedStage
    {
        get { return _cleardStage; }

        set
        {
            _cleardStage = value;

            Debug.Log($"{_cleardStage}{stageInfos.Count}");

            if(_cleardStage != 0 &&
               _cleardStage == stageInfos.Count )
            {
                isLastStage = true;
            }
        }
    }

    int _tilesSetDone;
    int _flaggedTilesCount;
    int _genratedMinesCount;
    int _openedNumsCount;
    int _heart;
    int _opendeMinesCount;
    int _currentStage;
    int _totalScore;
    int _cleardStage;

    Transform tiles;
    Node[,] nodes;

    float originalCameraorthographicSize;
    float scale;

    public bool isLastStage = false;
    int setMineCount;
    int width;
    int height;
    int totalTilesCount;
    int score;

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
    /// <summary>
    ///  초기화
    /// </summary>    
    /// 
    public void SetStageStatusFistPlay()
    {
        SetStageStatus(StageStatus.FirstStart);
    }
    public void SetStageStatusRetry()
    {
        SetStageStatus(StageStatus.Retry);
    }
    public void SetStageStatusNextStage()
    {
        SetStageStatus(StageStatus.NextStage);
    }
    public void SetStageStatus(StageStatus input)
    {
        status = input;
    }
    public void SetStage(int i)
    {
        CurrentStage = i;
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
        isSetAllTilseDone = false;
        StartCoroutine(E_GenerateTilse());
    }
    IEnumerator E_GenerateTilse()
    {
        yield return new WaitUntil(() => DestroyTiles());

        TilesSetDone = 0;
        FlaggedTilesCount = 0;
        OpenedNumsCount = 0;
        OpenedMinesCount = 0;
        GeneratedMinesCount = 0;
        transform.localScale = Vector3.one;
        transform.position = Vector3.zero;
        tiles.transform.localPosition = Vector3.zero;

        if (status == StageStatus.NextStage) CurrentStage++;

        _skinInfo = stageInfos[CurrentStage].skinInfo;
        GUICanvasManager.instance.GenerateHearts(maxHeart, _skinInfo);

        switch (status)
        {
            case StageStatus.FirstStart:
                Heart = startHeart;
                TotalScore = 0;
                ClearedStage = 0;
                break;
            case StageStatus.Retry:
                Heart = startHeart;
                TotalScore = 0;
                ClearedStage = 0;
                break;
            case StageStatus.NextStage:
                break;

            default: break;
        }

        if(status!=StageStatus.Retry)
        {
            LoadStageinfo();
            ResetScale();
            ResetCamera();
        }

        //TileNode 생성
        nodes = new Node[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Transform tile = Instantiate(node, new Vector3(i, j, 0), Quaternion.identity, tiles);
                nodes[i, j] = tile.GetComponent<Node>();
            }
        }

        //지뢰 생성
        for (int n = 0; n < setMineCount;)
        {
            int i = Random.Range(0, width);
            int j = Random.Range(0, height);
            if (nodes[i, j].nodeType == NodeType.Num)
            {
                nodes[i, j].nodeType = NodeType.Mine;
                GeneratedMinesCount++;
                n++;
            }
        }

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                nodes[i, j].Setup(stageInfos[CurrentStage].skinInfo);
            }
        }
        yield return new WaitUntil(() => isSetAllTilseDone);
        PlayManager.instance.ChangeGameStatusOnPlay();
        yield return null;
    }
    void LoadStageinfo()
    {
        int i = CurrentStage;

        width = stageInfos[i].StageSize;
        height = width / 2;

        score = stageInfos[i].Score;
        Heart += stageInfos[i].bonusHeart;
        setMineCount = stageInfos[i].MineCount;
        totalTilesCount = width * height;

    }
    void ResetScale()
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
        Debug.Log($" StageManager.SetWH 가로 : {width}, 세로 : {height}, 축적 : {scale}");
    }
    void ResetCamera()
    {
        gamePlayCamera.transform.position = new Vector3((width - 1) * 0.5f, (height - 1) * 0.5f, -10);
        gamePlayCamera.transform.position += Vector3.up * (1 / scale);
        gamePlayCamera.orthographicSize = originalCameraorthographicSize / scale;
        background.localScale = Vector3.one * 1f / scale;
    }
    /// <summary>
    ///  게임 플레이 이벤트
    /// </summary>
    /// <returns></returns>
    bool CheckGameClear()
    {
        if (isSetAllTilseDone)
        {
            if (_flaggedTilesCount + _openedNumsCount + _opendeMinesCount == totalTilesCount)
                return true;
            else return false;
        }

        return false;
    }

    void WinEvent()
    {
        ClearedStage++;
        PlayManager.instance.ChangeGameStatusWin();
    }

    void GameOver()
    {
        PlayManager.instance.ChangeGameStatusGameOver();
    }

}


