using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 7;
    private Transform tr;
    Vector3 move;


    private void Awake()
    {
        tr = transform;
    }
    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        move = new Vector3(h, 0, v); 
    }

    void FixedUpdate()
    {
        //tr.position += move * moveSpeed * Time.fixedDeltaTime;
        tr.Translate(move*moveSpeed*Time.fixedDeltaTime);
    }


}
