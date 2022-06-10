using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data_StageInfo", menuName = "ScriptableObjects/StageInfo")]
public class StageInfo : ScriptableObject
{
    [Header("�ε��� 0 ~")]
    public int StageIndex;
    [Header("Ŭ���� ���ʽ� ü�� 1 ~ 5")]
    public int bonusHeart;
    [Header("���� ����")]
    public int MineCount;
    [Header("���� Ÿ�� ����")]
    public int StageSize;
    [Header("��1���� ����")]
    public int Score;
    [Header("�������� Skin")]
    public SkinInfo skinInfo;
}
