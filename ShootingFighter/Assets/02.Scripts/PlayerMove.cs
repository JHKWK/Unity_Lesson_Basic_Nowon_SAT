using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 7;
    public Transform moveZone;

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

        if (tr.position.x < -moveZone.localScale.x/2 )
            if (h > 0) ;
            else h = 0;

        if (tr.position.x > moveZone.localScale.x / 2)
            if (h < 0) ;
            else h = 0;

        if (tr.position.z < -moveZone.localScale.z / 2)
            if (v > 0) ;
            else v = 0;

        if (tr.position.z > moveZone.localScale.z / 2)
            if (v < 0) ;
            else v = 0;


        move = new Vector3(h, 0, v);
    }

    void FixedUpdate()
    {
        //tr.position += move * moveSpeed * Time.fixedDeltaTime;  
        tr.Translate(move*moveSpeed*Time.fixedDeltaTime);
 
    }


}
