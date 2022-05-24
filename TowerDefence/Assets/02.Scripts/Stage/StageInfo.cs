using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 스테이지별 정보를 가지고 있음
/// </summary>

[System.Serializable]
public class StageInfo
{
    public int stage;
    public EnemySpawner.SpawnElement[] enemiesElements;
}
