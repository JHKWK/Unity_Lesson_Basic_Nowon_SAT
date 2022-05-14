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
    /// ���� ����Ʈ�� �������� �Լ�
    /// </summary>
    /// <param name="currentPointIndex"> ���� ����Ʈ �ε��� </param>
    /// <param name="nextPoint"> ��ȯ�� ���� ����Ʈ </param>
    /// <returns>��������Ʈ ȹ�� ���� : true / ���� :false </returns>
    public bool TryGetNextPoint(int currentPointIndex, out Transform nextPoint) // out -> ��������
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
