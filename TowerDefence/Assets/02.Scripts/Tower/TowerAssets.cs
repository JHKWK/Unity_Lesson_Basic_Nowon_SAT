using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAssets : MonoBehaviour
{
    static TowerAssets _instance;
    public static TowerAssets instance
    {
        get
        {
            if (_instance == null)
                _instance = Instantiate(Resources.Load<TowerAssets>("Assets/TowerAssets"));
            return _instance;
        }
    }

    public List<GameObject> towerPrefabs = new List<GameObject>();

    public static bool TryGetTowerPrefab(TowerType towerType, int towerUpgrageLevel,out GameObject towerPrefab)
    {
        towerPrefab = instance.towerPrefabs.Find(x => x.GetComponent<Tower>().info.type == towerType &&
                                                      x.GetComponent<Tower>().info.upgradeLevel == towerUpgrageLevel);
        return towerPrefab != null ? true : false;
    }

}
