using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo_Inverse : TileInfo
{
    public override void TileEvent()
    {
        Debug.Log($"index of this Tile : {index}, player will move reverse");
        DicePlayManager.instance.moveDir=-1;
    }

}
    
