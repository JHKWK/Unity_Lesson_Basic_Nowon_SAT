using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public Vector3 rangeCenter;
    public Vector3 rangeSize;

    public Quaternion target;

    public Transform SpwanSpot;
    public float SpwanTimeGap = 0.5f;
    private float SpwanTimer;
    private void Update()
    {
        Vector3 spawnPos = new Vector3(rangeCenter.x + Random.Range(-rangeSize.x / 2, rangeSize.x / 2),
                                       rangeCenter.y + Random.Range(-rangeSize.y / 2, rangeSize.y / 2),
                                       rangeCenter.z + Random.Range(-rangeSize.z / 2, rangeSize.z / 2)); //Vector3�� ��Ұ� 3���̹Ƿ� (x,y,z)�� ��� �����Ѵ�
        if (SpwanTimer < 0 )
        {
            Instantiate(EnemyPrefab, spawnPos, target);
            SpwanTimer = SpwanTimeGap;
        }
        else
            SpwanTimer -= Time.deltaTime;
    }

    private void OnDrawGizmosSelected() // ����Ƽ ������ ȭ�鿡 ǥ���� �ִ� ���
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(rangeCenter, rangeSize);
    }

}
