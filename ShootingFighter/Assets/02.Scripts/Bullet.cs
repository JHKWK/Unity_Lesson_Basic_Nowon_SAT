using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 dir = Vector3.forward;
    public float speed = 10f;
    Transform tr;

    private void Awake() =>
        tr = transform;

    private void FixedUpdate() =>
        tr.Translate(dir * speed * Time.fixedDeltaTime);
    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        if (go == null) return;

        if (go.layer == LayerMask.NameToLayer("Enemy"))
        {   

            /*Enemy enemy = go.GetComponent<Enemy>();*/
            /*enemy.DoDestroyEffect();*/

            go.GetComponent<Enemy>().DoDestroyEffect();
            Destroy(go);
            Destroy(gameObject);
        }
            
    }



}
