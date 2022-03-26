using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    static public PlayerHP instance;

    public int HP = 10;
    Transform HPgage;
    Vector3 HPPos;

    public void PlayerAttacked()
    {
        HP--;
    }

    private void Awake()
    {
        HPgage = transform;
        instance = this;
    }

    void Update()
    {
        if(HP <0) HP = 0;

        HPPos = new Vector3(HP, 10, 10);
        HPgage.localScale = HPPos;
    }


    
}
