using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform fireEffect;
    Transform tr;
    Transform target;
    float speed;
    float damage;
    private void Awake()
    {
        tr = transform;
        Transform tmpeffect = Instantiate(fireEffect, tr.position, tr.rotation);
    }

    private void FixedUpdate()
    {
        if(GetComponentInParent<Tower_MachineGun>() != null)
        {
            speed = GetComponentInParent<Tower_MachineGun>().bulletSpeed;
            tr.Translate(Vector3.forward * speed * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (col.gameObject.GetComponentInParent<Enemy>() != null)
            {
                if (GetComponentInParent<Tower_MachineGun>() != null) damage = GetComponentInParent<Tower_MachineGun>().damage;
                col.gameObject.GetComponentInParent<Enemy>().hp -= damage;
                StartCoroutine(ChangeColor(col.gameObject));
            }
        } 
        else Destroy(gameObject);
    }

    IEnumerator ChangeColor(GameObject tmpTr)
    {
        tr.position =new Vector3( 0,0,-5000);
        Color originalcolor;
        originalcolor = tmpTr.GetComponent<Renderer>().material.color;
        tmpTr.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        if (tmpTr != null)
            tmpTr.GetComponent<Renderer>().material.color = originalcolor;
        Destroy(gameObject);
        yield return null;
    }
}
