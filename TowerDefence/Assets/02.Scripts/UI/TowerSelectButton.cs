using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelectButton : MonoBehaviour
{
    [SerializeField] TowerInfo towerInfo;

    public void OnClick()
    {
        TowerHandler.instance.Setup(towerInfo);
    }

}
