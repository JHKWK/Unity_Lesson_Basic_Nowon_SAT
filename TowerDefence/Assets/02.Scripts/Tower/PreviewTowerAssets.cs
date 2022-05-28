using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewTowerAssets : MonoBehaviour
{
    static PreviewTowerAssets _instance;
    public static PreviewTowerAssets instance 
    {
        get
        {
            if(_instance == null)
            {
                _instance = Instantiate(Resources.Load<PreviewTowerAssets>("Assets/PreviewTowerAssets"));
            }
            return _instance;
        }
     }

    public List<GameObject> previewTowers = new List<GameObject>();

    public static GameObject GetPreviewTower (TowerType towerType, int towerLevel)
    {
        return instance.previewTowers.Find(x => x.GetComponent<PreviewTower>().TowerType == towerType &&
                                                x.GetComponent<PreviewTower>().towerLevel == towerLevel);
    }
}
