using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{
    public static TowerUI instance;

    float _offsetY = 2f;
    Node _node;
    Color _TextColorOrigin;

    [SerializeField] GameObject upgradeButton;
    [SerializeField] GameObject sellButton;
    [SerializeField] Text upgradePriceText;
    [SerializeField] Text sellPriceText;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;

        _TextColorOrigin = upgradePriceText.color;
        Clear();
    }

    public void Setup(Vector3 position, Node node)
    {
        _node = node;

        //위치 세팅
        transform.position = position + Vector3.up * _offsetY;

        if (transform.position.x > 6) transform.position += Vector3.left;
        if (transform.position.x < -11.5f) transform.position += Vector3.right;
        if (transform.position.z > 5) transform.position += Vector3.back; 
        if (transform.position.z < -5) transform.position += Vector3.forward;



        // 업그레이드 버튼 세팅
        if (TowerAssets.TryGetTowerPrefab(_node.towerInfo.type, _node.towerInfo.upgradeLevel + 1, out GameObject towerPrefab))
        {
            int upgradePrice = towerPrefab.GetComponent<Tower>().info.buildPrice;

            // 업그레이드 버튼 활성/비활성
            if(upgradePrice > LevelManager.instance.money)
            {
                upgradePriceText.color = Color.red;
                upgradeButton.GetComponent<Button>().interactable = false;
            }
            else
            {
                upgradePriceText.color = _TextColorOrigin;
                upgradeButton.GetComponent <Button>().interactable = true;
            }

            upgradeButton.SetActive(true);
            upgradePriceText.text = upgradePrice.ToString();
        }
        else
        {
            upgradeButton.SetActive(false);
        }

        //팔기 버튼 세팅
        sellPriceText.text= _node.towerInfo.sellPrice.ToString();
        gameObject.SetActive(true);
    }

    public void Clear()
    {
        upgradePriceText.text = "";
        sellPriceText.text = "";
        gameObject.SetActive(false);
    }

    public void Upgrade()
    {
        if (TowerAssets.TryGetTowerPrefab(_node.towerInfo.type,
                                  _node.towerInfo.upgradeLevel + 1,
                                  out GameObject towerPrefab))
        {
            _node.DestoryTower(); // 기존타워 철거
            _node.BuildTower(towerPrefab); //다음레벨 타워 건설
            LevelManager.instance.money -= towerPrefab.GetComponent<Tower>().info.buildPrice;
        }
        Clear();
    }

    public void Sell()
    {
        LevelManager.instance.money += _node.towerInfo.sellPrice;
        _node.DestoryTower();
        Clear();
    }
}
