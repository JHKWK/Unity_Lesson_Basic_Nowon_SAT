using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExploding : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(selfDestroy());
    }
    IEnumerator selfDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        yield return null;
    }
}
