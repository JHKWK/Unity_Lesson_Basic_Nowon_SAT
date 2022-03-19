using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Vector3 dir = Vector3.back;
    public float speed = 2;
    Transform tr;

    private void Awake() =>
        tr = transform;

    private void FixedUpdate() =>
        tr.Translate(dir * speed * Time.fixedDeltaTime);
}
