using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo_Dice : TileInfo
{
    public override void TileEvent()
    {
        Debug.Log($"index of this Tile : {index}, Increase dice num +1");
        DicePlayManager.instance.diceNum++;
    }

}
