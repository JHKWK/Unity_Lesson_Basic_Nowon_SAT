using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum HitType
{
    Bad,
    Miss,
    Good,
    Great,
    Perfect,
}
public class Note : MonoBehaviour
{
    public KeyCode keyCode;
    public float speed = 1;
    Transform tr;


    //==================================================================================
    //********************************* Public Methods *********************************
    //==================================================================================

    public void Hit(HitType hitType)
    {
        switch (hitType)
        {
            case HitType.Bad:
                break;
            case HitType.Miss:
                break;
            case HitType.Good:
                break;
            case HitType.Great:
                break;
            case HitType.Perfect:
                break;
            
        }
    }

    //==================================================================================
    //********************************* Private Methods *********************************
    //==================================================================================



    private void Awake()
    {
        tr = transform;
    }

    private void FixedUpdate()
    {
        tr.Translate(Vector2.down * speed * Time.fixedDeltaTime);
        // tr.position += Vector3.down * speed * Time.fixedDeltaTime;
    }
}

