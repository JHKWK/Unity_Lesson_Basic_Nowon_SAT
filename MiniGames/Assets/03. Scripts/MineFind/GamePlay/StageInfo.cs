using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data_StageInfo", menuName = "ScriptableObjects/StageInfo")]
public class StageInfo : ScriptableObject
{
    [Header("인덱스 0 ~")]
    public int StageIndex;
    [Header("클리어 보너스 체력 1 ~ 5")]
    public int bonusHeart;
    [Header("지뢰 갯수")]
    public int MineCount;
    [Header("가로 타일 개수")]
    public int StageSize;
    [Header("블럭1개당 점수")]
    public int Score;
    [Header("스테이지 Skin")]
    public SkinInfo skinInfo;
}
