using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveZone
    : MonoBehaviour
{    
    private void OnDrawGizmosSelected() // ����Ƽ ������ ȭ�鿡 ǥ���� �ִ� ���
    {
        Transform box = gameObject.GetComponent<Transform>();
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(box.position,box.localScale);
    }

}
