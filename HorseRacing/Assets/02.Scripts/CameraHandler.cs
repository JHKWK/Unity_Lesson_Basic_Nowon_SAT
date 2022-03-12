using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private Transform tr;
    private List<Transform> players = new List<Transform>();
    private int targetIndex = 0;

    private Vector3 offset = new Vector3(0, 2, -4);

    private void Awake()
    {
        tr = transform;        
    }

    void Start()
    {
        foreach (var item in GamePlay.instance.players)
        {
            players.Add(item.transform);            
        }
    }

void Update()
    {
        if (GamePlay.instance.onPlay)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                SwtichTarget();              
            }
        }
        
    }

    private void FixedUpdate()
    {
        FollowTarget();
    }

    private void SwtichTarget()
    {
        targetIndex++;
        if (targetIndex > players.Count - 1)
        {
            targetIndex = 0;
        }
    }

    private void FollowTarget()
    {
        tr.position = players[targetIndex].position + offset;

    }
}
