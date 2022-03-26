using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DicePlayManager : MonoBehaviour
{
    private int currentTileIndex;
    private int _diceNum;
    private int _goldenDiceNum;
    private int _starScore;

    public int diceNum
    {
        set
        {
            if(value >= 0)
            {
                _diceNum = value;
                diceText.text = _diceNum.ToString();
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

    public Text diceText;
    public Text goldenDiceText;
    public Text starScoreText;

    public int diceNumInit;
    public int golednDiceNumInit;
    



    public List<Transform> mapTiles;

    private void Awake()
    {
        starScore = 0;
        diceNum = diceNumInit;
        goldenDiceNum = golednDiceNumInit;
    }
    public void RollADice()
    {
        if (diceNum < 1) return;
        diceNum --;
        int diceValue = Random.Range(1, 7);
        MovePlayer(diceValue);
    }
    private void MovePlayer(int diceValue)
    {   
        currentTileIndex += diceValue;
        if(currentTileIndex >= mapTiles.Count) currentTileIndex -= mapTiles.Count;

        Player.intance.Move(GetTilePosition(currentTileIndex));
    }
    private Vector3 GetTilePosition(int tileIndex)
    {
        return mapTiles[tileIndex].position;
    }
}