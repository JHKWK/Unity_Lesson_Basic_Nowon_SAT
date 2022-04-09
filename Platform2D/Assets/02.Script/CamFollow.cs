using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    Transform tr;
    public Transform player;

    private void Awake()
    {
        tr= transform;        
    }
    private void Update()
    {
        tr.position = player.position;
    }
}
