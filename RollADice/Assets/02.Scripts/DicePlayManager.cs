using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DicePlayManager : MonoBehaviour
{
    public static DicePlayManager instance;

    private int currentTileIndex; //현재 칸 인덱스
    private int _diceNum; //남은 주사위 총 갯수
    private int _goldenDiceNum; //황금 주사위 남은 갯수
    private int _starScore;

    public int moveDir = 1;
    //프로퍼티 get; set; 다른객체에서 현재 객체에 접근하기위함.
    //변수와 함수의 중간 : 변수처럼 취급하나 함수처럼 동작함.
    public int diceNum
    {
        set
        {
            if(value >= 0) //남은 주사위 갯수가 0이상으로만 셋 가능
            {
                _diceNum = value;
                diceText.text = _diceNum.ToString(); // Text엄데이트
            }
        }
        get
        {
            return _diceNum;
        }
    }

    public int goldenDiceNum
    {
        set
        {
            if (value >= 0)
            {
                _goldenDiceNum = value;
                goldenDiceText.text = _goldenDiceNum.ToString();
            }
        }
        get
        {
            return _goldenDiceNum;
        }
    }

    public int starScore
    {
        set
        {
            if (value >= 0)
            {
                _starScore = value;
                starScoreText.text = _starScore.ToString();
            }
        }
        get
        {
            return _starScore;
        }
    }

    public Text diceText; //남은 주사위 갯수 UI_Text
    public Text goldenDiceText;
    public Text starScoreText;

    public int diceNumInit; // 주사위 초기값
    public int golednDiceNumInit;

    public List<Transform> mapTiles; // 타일의 좌표 리스트 : 플레이어가 이동할 위치를 받기위해 필요하다
    public Coroutine animationCoroutine;

    private void Awake()
    {
        instance = this;
        starScore = 0;
        diceNum = diceNumInit;
        goldenDiceNum = golednDiceNumInit;
        animationCoroutine = null;
    }
    public void RollADice()
    {
        if (diceNum < 1 || animationCoroutine != null) return;
        //if (animationCoroutine != null) return;
        diceNum --;
        int diceValue = Random.Range(1, 7);
        animationCoroutine = StartCoroutine(DiceAnimationUI.Instance.E_DiceAnimation(diceValue,this,MovePlayer));
        moveDir = 1;
    } // 주사위를 굴리고 플레이어를 이동시킴
    public void RollAGoldenDice(int diceValue)
    {
        if (goldenDiceNum < 1 || animationCoroutine != null) return;
        goldenDiceNum --;
        animationCoroutine = StartCoroutine(DiceAnimationUI.Instance.E_DiceAnimation(diceValue, this, MovePlayer));
    }
    private void MovePlayer(int diceValue)
    {
        int previousTileIndex = currentTileIndex;
        currentTileIndex += diceValue;

        CheckPlayerPasssStarTile(previousTileIndex, currentTileIndex);

        if (currentTileIndex >= mapTiles.Count) currentTileIndex -= mapTiles.Count;

        Player.intance.Move(GetTilePosition(currentTileIndex));
        mapTiles[currentTileIndex].GetComponent<TileInfo>().TileEvent();

    } 
    private void CheckPlayerPasssStarTile(int previousIndex, int currentTileIndex)
    {
        for(int i = previousIndex + 1; i <= currentTileIndex; i++)
        {
            int tmpindex = i;
            if (tmpindex>= mapTiles.Count) tmpindex -= mapTiles.Count;
            if ( mapTiles[tmpindex].TryGetComponent(out TileInfo_Star tmpStarTile) )
                starScore += tmpStarTile.starValue++;
        }
    }
    private Vector3 GetTilePosition(int tileIndex)
    {
        return mapTiles[tileIndex].position;
    }
}