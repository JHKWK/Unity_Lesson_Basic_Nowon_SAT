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
        // 건설할 타워가 선택 되어있고 현재 노드에 건설된 타워가 없다면
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
                //타워 가져오는데 실패
                throw new System.Exception("타워 프리팹을 가져오는데 실패함. 타워타입과 레벨확인 요망");
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
