using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoUI : MonoBehaviour
{
    public EnemySpawner enemySpawner;
    public Text currentLevelText;
    public Text currentStageText;
    public Text moneyText;
    public Text killedEnemyText;

    int _currentLevel;

    private void Update()
    {
        _currentLevel = (enemySpawner.currentLevel) + 1;
        currentLevelText.text =$"  Current Level : { _currentLevel.ToString()}";
        currentStageText.text =$"  Currnet Stage : { enemySpawner.currentStage.ToString()}";
        moneyText.text = $"  Money : {LevelManager.instance.money.ToString()}";
        killedEnemyText.text = $"  Killed Enemy : {LevelManager.instance.killedEnemy.ToString()}";
    }

}
