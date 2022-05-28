using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelectButton : MonoBehaviour
{
    [SerializeField] TowerInfo towerInfo;

    public void OnClick()
    {
        if (towerInfo.buildPrice <= LevelManager.instance.money)
            TowerHandler.instance.Setup(towerInfo);
        else
        {
            // todo "���̺����մϴ�" �˾�â ����
        }


    }

}
