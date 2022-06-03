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
    private Transform _worldZero;


    private void Awake()
    {
        /*
        _worldZero = new GameObject().transform;

        _worldZero = WorldZero.instance.transform;
        */

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

        //Ÿ�ٿ� ���� �ߴ��� üũ
        if(Vector3.Distance(tr.position, targetPos) < posTolerance)
        {
            if (WayPoints.instance.TryGetNextPoint(currentPointIndex, out nextWayPoint))
            {
                if(WayPoints.instance.TryGetNextPoint(currentPointIndex, out nextWayPoint))
                {
                    currentPointIndex++;
                    
                    // _worldZero�� �ʱ�ȭ ���� �ʰ� ������� �� �����ǰ� �ִ� ����� ��������?????
                    //ui.parent = _worldZero;
                    ui.parent = null;//�� �۵��Ǵ°��ΰ�
                    Debug.Log(_worldZero); // Null UnityEngine.Debug:Log(object) 

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
