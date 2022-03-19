using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Transform mainView;
    public Transform endingView;
    private Transform tr;
    private List<Transform> viewPoints = new List<Transform>();
    private int targetIndex = 0;

    public Vector3 positionOffset = new Vector3(0, 1, -1.5f);
    public Vector3 angleOffset = new Vector3(15, 0, 0);
    

    private void Awake()
    {
        tr = transform;
        
    }

    void Start()
    {
        viewPoints.Add(mainView);

        foreach (var item in GamePlay.instance.players)
        {
            viewPoints.Add(item.transform);            
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

        else if (GamePlay.instance.raceEnd)
        {
            tr.position = endingView.position;
            tr.eulerAngles = endingView.eulerAngles;
        }
    }

    private void FixedUpdate()
    {
        FollowTarget();
    }

    private void SwtichTarget()
    {
        targetIndex++;
        if (targetIndex > viewPoints.Count - 1)
        {
            targetIndex = 0;
        }
    }

    private void FollowTarget()
    {
        if (targetIndex == 0)
        {
            tr.position = viewPoints[targetIndex].position;
            tr.eulerAngles = viewPoints[targetIndex].eulerAngles;
        }
        else
        {
            tr.position = viewPoints[targetIndex].position + positionOffset;
            tr.eulerAngles = viewPoints[targetIndex].eulerAngles + angleOffset;
        }
    }
}
