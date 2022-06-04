using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float posTolerance = 0.01f;

    private Transform tr;
    private int currentPointIndex = 0;
    private Transform nextWayPoint;
    private float originPosY;
    private Transform ui;

    private void Awake()
    {

        tr = transform;
        originPosY = transform.position.y;

        ui = tr.Find("UI");
    }

    private void Start()
    {
        nextWayPoint =  WayPoints.instance.GetFirstWayPoint();
    }

    private void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(nextWayPoint.position.x,
                                        originPosY,
                                        nextWayPoint.position.z);

        Vector3 dir = (targetPos - tr.position).normalized;

        //Å¸°Ù¿¡ µµÂø Çß´ÂÁö Ã¼Å©
        if(Vector3.Distance(tr.position, targetPos) < posTolerance)
        {
            if (WayPoints.instance.TryGetNextPoint(currentPointIndex, out nextWayPoint))
            {
                if(WayPoints.instance.TryGetNextPoint(currentPointIndex, out nextWayPoint))
                {
                    currentPointIndex++;

                    ui.parent = null;
                    tr.LookAt(nextWayPoint);
                    ui.parent = tr;
                }
            }
            else
            {
                OndReachedToEnd();
            }
        }

        tr.Translate(dir * moveSpeed * Time.fixedDeltaTime, Space.World);
        //tr.position += dir * moveSpeed * Time.fixedDeltaTime;
    }

    private void OndReachedToEnd()
    {
        this.gameObject.SetActive(false);
    }
}
