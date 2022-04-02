using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TileInfo_Star : TileInfo
{
    public Text starValueText;
    public int starValueInit;
    private int _starValue;

    private void Awake()
    {
        starValue = starValueInit;
    }

    public int starValue
    {
        set
        {
            _starValue = value;
            starValueText.text=_starValue.ToString();
        }
        get { return _starValue; }
    }



    public override void TileEvent()
    {
        Debug.Log($"index of this Tile : {index}, Increase star value +1");
        starValue++;
    }

}
