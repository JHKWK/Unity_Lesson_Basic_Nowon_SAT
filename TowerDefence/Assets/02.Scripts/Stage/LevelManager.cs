using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int level;
    public int money;
    LevelInfo info;

    public static LevelManager instance;




    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;

        Setup();
    }
    void Setup()
    {
        info = LevelInfoAssets.GetLevelInfo(level);
        money = info.initMoney;
    }

}
