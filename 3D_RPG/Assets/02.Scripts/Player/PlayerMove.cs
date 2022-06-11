using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3;
    [SerializeField] float dashSpeed = 5f;
    [SerializeField] float gravity = 9.81f;

    [SerializeField] Animator playerAnimator;
    GroundSensor groundSensor;
    Rigidbody rb;
    Vector3 _move;
    
 

    void SetMove(float x, float z, float speed)
    {
        _move.x = x * speed;
        _move.z = z * speed;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        groundSensor = GetComponentInChildren<GroundSensor>();
    }

    private void Update()
    {


        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        playerAnimator.SetFloat("h", h);
        playerAnimator.SetFloat("v", v);

        if ((h == 0) && (v == 1) &&
            Input.GetKey(KeyCode.LeftShift))
        {
            SetMove(h, v*2, dashSpeed);
            playerAnimator.SetFloat("RunForwardBlend", 1.0f);
        }
        else
            SetMove(h, v*2, moveSpeed);

    }

    private void FixedUpdate()
    {
        if (groundSensor.isOn == false)
            rb.position += Vector3.down * gravity * Time.fixedDeltaTime;
            //rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);

        rb.position += _move * Time.fixedDeltaTime;
    }
}
