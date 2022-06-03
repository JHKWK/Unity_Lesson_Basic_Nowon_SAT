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

        //��ġ ����
        transform.position = position + Vector3.up * _offsetY;

        if (transform.position.x > 6) transform.position += Vector3.left;
        if (transform.position.x < -11.5f) transform.position += Vector3.right;
        if (transform.position.z > 5) transform.position += Vector3.back; 
        if (transform.position.z < -5) transform.position += Vector3.forward;



        // ���׷��̵� ��ư ����
        if (TowerAssets.TryGetTowerPrefab(_node.towerInfo.type, _node.towerInfo.upgradeLevel + 1, out GameObject towerPrefab))
        {
            int upgradePrice = towerPrefab.GetComponent<Tower>().info.buildPrice;

            // ���׷��̵� ��ư Ȱ��/��Ȱ��
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

        //�ȱ� ��ư ����
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
            _node.DestoryTower(); // ����Ÿ�� ö��
            _node.BuildTower(towerPrefab); //�������� Ÿ�� �Ǽ�
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
