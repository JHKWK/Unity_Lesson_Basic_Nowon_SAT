using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int level;
    public int money;
    public int killedEnemy;
    LevelInfo info;

    public static LevelManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;

        money = 1000;
        Setup();
    }
    void Setup()
    {
            info = LevelInfoAssets.GetLevelInfo(level);
            money += info.initMoney;
    }

    public void MoveToNextLevel()
    {
        if (LevelInfoAssets.GetLevelInfo(level+1))
        {
            level++;
            Setup();
            GameObject[] enenmySpawners = GameObject.FindGameObjectsWithTag("EnemySpawner");
            Debug.Log(enenmySpawners[0].name);
            for (int i = 0; i < enenmySpawners.Length; i++)
            {
                enenmySpawners[i].GetComponent<EnemySpawner>().Setup(level);
            }
        }
        else
        {
            Debug.Log("다음 레벨이 없습니다");
            return;
        }
    }

    public void MoveToNLevel(int a)
    {
        if (LevelInfoAssets.GetLevelInfo(a))
        {
            level=a;
            Setup();
            GameObject[] enenmySpawners = GameObject.FindGameObjectsWithTag("EnemySpawner");
            Debug.Log(enenmySpawners[0].name);
            for (int i = 0; i < enenmySpawners.Length; i++)
            {
                enenmySpawners[i].GetComponent<EnemySpawner>().Setup(level);
            }
        }
        else
        {
            Debug.Log("지정한 레벨이 없습니다");
            return;
        }
    }

}
