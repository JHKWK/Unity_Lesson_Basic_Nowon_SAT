using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveZone
    : MonoBehaviour
{    
    private void OnDrawGizmosSelected() // 유니티 에디터 화면에 표시해 주는 기능
    {
        Transform box = gameObject.GetComponent<Transform>();
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(box.position,box.localScale);
    }

}
