using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float startDelay = 1f;
    [HideInInspector] public int currentLevel;
    [HideInInspector] public int currentStage;

    [System.Serializable]
    public class SpawnElement
    {
        public GameObject prefab;
        public int num;
        public float delay;
    }
    [SerializeField] private SpawnElement[][] spawnElements; 
    float[][] timers;
    int[][] counts;



    private void Awake()
    {
        Setup(0);
    }

    public void Spawn()
    {
        if (currentStage < spawnElements.Length)
        {
            StartCoroutine(E_Spawn());
        }
    }
    public void Setup(int level)
    {
        currentStage = 0;
        currentLevel = level;
        //현재 레벨에 대한 모든 스테이지 정보 가져옴
        StageInfo[] tmpStageInfos = LevelInfoAssets.GetAllStageInfos(currentLevel);

        //소환해야하는 에너미의 배열의 스테이지 크기 할당
        spawnElements = new SpawnElement[tmpStageInfos.Length][];

        //소환해야 하는 스테이지별 에너미 목록 할당
        for (int i = 0; i < tmpStageInfos.Length; i++)
        {
            spawnElements[i] = tmpStageInfos[i].enemiesElements;
        }
        timers = new float[spawnElements.Length][];
        counts = new int[spawnElements.Length][];

        for (int i = 0; i < spawnElements.Length; i++)
        {
            timers[i] = new float[spawnElements[i].Length];
            counts[i] = new int[spawnElements[i].Length];

            for (int j = 0; j < spawnElements[i].Length; j++)
            {
                timers[i][j] = spawnElements[i][j].delay;
                counts[i][j] = spawnElements[i][j].num;
            }
        }

    }
    IEnumerator E_Spawn()
    {
        int tmpStage = currentStage;
        currentStage++;
        yield return new WaitForSeconds(startDelay);

        bool isDone = false;
        while(isDone == false)
        {
            isDone = true;

            for (int i = 0; i < spawnElements[tmpStage].Length; i++)
            {
                //소환할 것이 남아있는지
                if (counts[tmpStage][i] > 0)
                {
                    isDone = false;

                    //소환 딜레이 체크
                    if (timers[tmpStage][i] < 0)
                    {
                        Instantiate(spawnElements[tmpStage][i].prefab,
                                    WayPoints.instance.GetFirstWayPoint().position,
                                    Quaternion.identity);
                        counts[tmpStage][i]--;
                        timers[tmpStage][i] = spawnElements[tmpStage][i].delay;
                    }
                    else
                        timers[tmpStage][i] -= Time.deltaTime;
                }
                yield return null;
            }
        }
    }
}
