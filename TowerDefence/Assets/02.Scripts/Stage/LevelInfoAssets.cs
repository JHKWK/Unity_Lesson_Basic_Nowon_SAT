using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 레벨에 대한 정보 에셋을 가져다 쓰기 위한 클래스
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
    /// 특정 레벨의 특정 스테이지 정보를 반환함
    /// </summary>
    /// <param name="level"> 검색할 레벨 </param>
    /// <param name="stage"> 검색할 스테이지 </param>
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
    /// 특정 레벨의 모든 스테이지 정보를 반환 함
    /// </summary>
    /// <param name="level">검색할 레벨</param>
    /// <returns></returns>
    public static StageInfo[] GetAllStageInfos(int level)
    {
        //찾고자 하는 레벨 정보 검색
        LevelInfo levelInfo = instance.levelInfos.Find(x => x.level == level);

        //레벨정보 검색 성공 시
        if(levelInfo != null)
        {
            return levelInfo.stageInfos.ToArray();
        }
        return null;
    }

}
