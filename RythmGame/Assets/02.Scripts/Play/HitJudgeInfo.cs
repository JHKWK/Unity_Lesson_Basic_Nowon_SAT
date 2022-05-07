using UnityEngine;

[CreateAssetMenu(menuName = "Hit Judge Info")]

public class HitJudgeInfo : ScriptableObject
{
    [Header("Bad 판정 높이")]
    public float hitJudgeHeigth_Bad;
    [Header("Miss 판정 높이")]
    public float hitJudgeHeigth_Miss;
    [Header("Good 판정 높이")]
    public float hitJudgeHeigth_Good;
    [Header("Great 판정 높이")]
    public float hitJudgeHeigth_Great;
    [Header("Cool 판정 높이")]
    public float hitJudgeHeigth_Perfect;
}
