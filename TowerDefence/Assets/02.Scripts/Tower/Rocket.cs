using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed;
    public LayerMask touchLayer;
    public LayerMask targetLayer;
    public float explosionRange;

    float _damage;
    Vector3 _moveVec;
    Vector3 _velocity;
    Transform tr;

    void Explode()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, explosionRange,targetLayer);
        foreach (Collider col in cols)
        {
            if (col.GetComponentInParent<Enemy>()!= null)
                col.GetComponentInParent<Enemy>().hp -= _damage;
        }
        //Destroy(gameObject);
    }

    public void Setup(Vector3 dir, float damage)
    {
        tr.LookAt(dir);
        _moveVec = tr.forward * speed;
        _damage = damage;
    }

    private void Awake()
    {
        tr = transform;
    }

    private void Update()
    {
        Collider[] cols = Physics.OverlapSphere(tr.position, 1f, touchLayer);
        if (cols.Length > 0)
        {
            Explode();
        }
    }

    private void FixedUpdate()
    {
        tr.Translate( _moveVec * Time.fixedDeltaTime );
    }
}
