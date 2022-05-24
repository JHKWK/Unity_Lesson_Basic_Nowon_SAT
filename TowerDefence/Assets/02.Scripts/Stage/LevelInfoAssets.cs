using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ������ ���� ���� ������ ������ ���� ���� Ŭ����
/// </summary>
public class LevelInfoAssets : MonoBehaviour
{
    static LevelInfoAssets _intance;
    public static LevelInfoAssets instance
    {
        get
        {
            if (_intance == null)
                _intance = Instantiate(Resources.Load<LevelInfoAssets>("Assets/LevelInfoAssets"));
            return _intance;
        }
    }

    public List<LevelInfo> levelInfos = new List<LevelInfo>();
    /// <summary>
    /// Ư�� ������ Ư�� �������� ������ ��ȯ��
    /// </summary>
    /// <param name="level"> �˻��� ���� </param>
    /// <param name="stage"> �˻��� �������� </param>
    /// <returns></returns>
    public static StageInfo GetStageInfo(int level, int stage)
    {
        LevelInfo levelinfo = instance.levelInfos.Find(x => x.level == level);
        if (levelinfo != null)
        {
            return levelinfo.stageInfos.Find(x => x.stage == stage);
        }
        return null;
    }
    /// <summary>
    /// Ư�� ������ ��� �������� ������ ��ȯ ��
    /// </summary>
    /// <param name="level">�˻��� ����</param>
    /// <returns></returns>
    public static StageInfo[] GetAllStageInfos(int level)
    {
        //ã���� �ϴ� ���� ���� �˻�
        LevelInfo levelInfo = instance.levelInfos.Find(x => x.level == level);

        //�������� �˻� ���� ��
        if(levelInfo != null)
        {
            return levelInfo.stageInfos.ToArray();
        }
        return null;
    }

}
