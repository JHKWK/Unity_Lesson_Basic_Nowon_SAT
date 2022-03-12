using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotate : MonoBehaviour
{
    private Transform tr;
    public Vector3 dir = Vector3.up;
    public float minspeed;
    public float maxspeed;

    Vector3 rotate;

    private void Awake()
    {
       tr = GetComponent<Transform>();
    }
    void FixedUpdate()
    {
        float rotateSpeed = Random.Range( minspeed , maxspeed);
        rotate = dir * rotateSpeed * Time.fixedDeltaTime;
        tr.Rotate(rotate);
    }
}
