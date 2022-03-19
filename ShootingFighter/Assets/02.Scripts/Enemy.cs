using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject destroyEffect;

    public void DoDestroyEffect()
    {
        GameObject go = Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(go, 1);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"trggerd! {other.gameObject.name}");
        //Destroy(other.gameObject);
        Destroy(other);
    }

}
