using UnityEngine;

[CreateAssetMenu(menuName = "Hit Judge Info")]

public class HitJudgeInfo : ScriptableObject
{
    [Header("Bad ���� ����")]
    public float hitJudgeHeigth_Bad;
    [Header("Miss ���� ����")]
    public float hitJudgeHeigth_Miss;
    [Header("Good ���� ����")]
    public float hitJudgeHeigth_Good;
    [Header("Great ���� ����")]
    public float hitJudgeHeigth_Great;
    [Header("Cool ���� ����")]
    public float hitJudgeHeigth_Perfect;
}
