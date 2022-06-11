using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{

    public Transform player;


    Transform tr;



    private void Awake()
    {
        tr= transform;
        player.position=tr.position;
    }
}
