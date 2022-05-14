using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    public static WayPoints instance;

    Transform[] points;

    public Transform GetFirstWayPoint()
    {
        return points[0];
    }

    /// <summary>
    /// 다음 포인트를 가져오는 함수
    /// </summary>
    /// <param name="currentPointIndex"> 현재 포인트 인덱스 </param>
    /// <param name="nextPoint"> 반환할 다음 포인트 </param>
    /// <returns>다음포인트 획득 성공 : true / 실패 :false </returns>
    public bool TryGetNextPoint(int currentPointIndex, out Transform nextPoint) // out -> 참조형식
    {
        nextPoint = null;

        if(currentPointIndex < points.Length - 1)
        {
            nextPoint = points[currentPointIndex+1];
            return true;
        }
        return false;
    }

    private void Awake()
    {
        if(instance != null) Destroy(instance);
        instance = this;

        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
            points[i] = transform.GetChild(i);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++) points[i] = transform.GetChild(i);
        for (int i = 0; i < points.Length-1; i++) Gizmos.DrawLine(points[i].position, points[i + 1].position);
    }
}
