using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{   
    public float towerOffsetY;

    Tower towerBuilt;
    public Renderer render;

    private Color originalColor;
    public Color buildAvailableColor;
    public Color buildNotAvailableColor;
    public TowerInfo towerInfo
    {
        get { return towerBuilt.info; }
    }

    public void BuildTower(GameObject towerPrefab)
    {
        towerBuilt = Instantiate(towerPrefab,
                         transform.position + Vector3.up * towerOffsetY,
                         Quaternion.identity,
                         transform).GetComponent<Tower>();
    }

    public void DestoryTower()
    {
        Destroy(towerBuilt.gameObject);
        towerBuilt = null;
    }
        private void Awake()
        {
            render = GetComponent<Renderer>();
            originalColor = render.material.color;
        }

    private void OnMouseEnter()
    {
        if(TowerHandler.instance.isSeleted)
        {
            TowerHandler.instance.transform.position = transform.position + Vector3.up * towerOffsetY;

            if(towerBuilt == null)
            {
                render.material.color = buildAvailableColor;
            }
            else
            {
                render.material.color = buildNotAvailableColor;
            }
        }
    }
    private void OnMouseExit()
    {
        TowerHandler.instance.sendFar();
        render.material.color = originalColor;
    }

    private void OnMouseUp()
    {
        // �Ǽ��� Ÿ���� ���� �Ǿ��ְ� ���� ��忡 �Ǽ��� Ÿ���� ���ٸ�
        if (TowerHandler.instance.isSeleted &&
            towerBuilt == null)
        {
            TowerInfo info = TowerHandler.instance.selectedTowerInfo;
            if (TowerAssets.TryGetTowerPrefab(info.type, info.upgradeLevel, out GameObject towerPrefab))
            {
                BuildTower(towerPrefab);
                TowerHandler.instance.Clear();
            }
            else
            {
                //Ÿ�� �������µ� ����
                throw new System.Exception("Ÿ�� �������� �������µ� ������. Ÿ��Ÿ�԰� ����Ȯ�� ���");
            }
        }
        else if (TowerHandler.instance.isSeleted == false &&
                towerBuilt != null)
        {
            TowerUI.instance.Setup(towerBuilt.transform.position, this);
        }
    }
    private void OnMouseDown()
    {

    }

}
