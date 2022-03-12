using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermove : MonoBehaviour
{
    private Transform tr;
    public float distance;
    public Vector3 dir = Vector3.forward;
    public float minSpeed;
    public float maxSpeed;
    public bool doMove = false;

    Vector3 move;
    

    private void Awake()
    {
        tr = GetComponent<Transform>();
    }
    void FixedUpdate()
    {
        if (doMove)
            Move();
    }

    private void Move()
    {
        float moveSpeed = Random.Range(minSpeed,maxSpeed);
        move = dir * moveSpeed * Time.fixedDeltaTime ;
        tr.Translate(move);
        distance += move.magnitude;
    }


}
