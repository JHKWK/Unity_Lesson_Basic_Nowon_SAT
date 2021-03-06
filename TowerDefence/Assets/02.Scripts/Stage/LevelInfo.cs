using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data_LevelInfo", menuName = "ScriptableObjects/LevelInfo")]
public class LevelInfo : ScriptableObject
{
    public int level;
    public int initMoney;
    public List<StageInfo> stageInfos = new List<StageInfo>();
}

