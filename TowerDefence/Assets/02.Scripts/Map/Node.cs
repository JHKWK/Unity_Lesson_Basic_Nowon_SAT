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

}
