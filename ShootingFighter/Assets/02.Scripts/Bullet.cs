using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 dir = Vector3.forward;
    public float speed = 10f;
    public int damage = 10;
    Transform tr;

    private void Awake()
    {
        tr = transform;
    }
    

    private void FixedUpdate() =>
        tr.Translate(dir * speed * Time.fixedDeltaTime);
    private void OnTriggerEnter(Collider other)
    {
        CapsuleCollider collider = gameObject.GetComponent<CapsuleCollider>();
        GameObject renderer = transform.Find("Renderer").gameObject; 
        GameObject go = other.gameObject;

        if (go == null) return;

        if (go.layer == LayerMask.NameToLayer("Enemy"))
        {
            go.GetComponent<Enemy>().hp -= damage;
                        
            speed = 0;
            Destroy(collider);
            Destroy(renderer);
            Destroy(gameObject,2);
        }
            
    }



}
