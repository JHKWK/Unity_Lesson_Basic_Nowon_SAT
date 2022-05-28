using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerHandler : MonoBehaviour
{
    public static TowerHandler instance;
    public GameObject previewTower;


    public TowerInfo selectedTowerInfo;
    public bool isSeleted
    {
        get { return selectedTowerInfo != null ? true : false; }
        set { }
    }

    public void Setup(TowerInfo towerInfo)
    {
        selectedTowerInfo = towerInfo;
        gameObject.SetActive(true);
        if(previewTower != null) Destroy(previewTower);
        previewTower = Instantiate(PreviewTowerAssets.GetPreviewTower(selectedTowerInfo.type,selectedTowerInfo.upgradeLevel), transform);
    }

    public void Clear()
    {
        selectedTowerInfo = null;
        gameObject.SetActive(false);
        if (previewTower != null) Destroy(previewTower);
    }

    public void sendFar()
    {
        transform.position = new Vector3(0,-100,0);
    }

    private void Awake()
    {
        if(instance != null)
            Destroy(instance);

        instance = this;
        gameObject.SetActive(false);
    }

    private void Update()
    {

    }
}
