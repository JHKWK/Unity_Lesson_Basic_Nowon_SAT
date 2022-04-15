using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlatform : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject platform; 
    Vector2 platformMoveVector;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (GetComponent<PlayerController>().state == PlayerController.PlayerState.Fall) platform = null;
    }
    private void FixedUpdate()
    {
        if (platform)
        {
            platformMoveVector = platform.GetComponentInParent<PlatformMove>().moveVector;
            rb.position += platformMoveVector;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponentInParent<PlatformMove>())
            platform = collision.collider.gameObject;
        else platform = null;
    }

}
