using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo_GoldenDice : TileInfo
{
    public override void TileEvent()
    {
        Debug.Log($"index of this Tile : {index}, Increase golden dice num +1");
        DicePlayManager.instance.goldenDiceNum++;
    }
}