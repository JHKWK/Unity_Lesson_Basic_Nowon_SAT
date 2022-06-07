using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float scaleSpeed;
    public float moveSpeed;
    public float TargetScale;

    Transform tr;
    bool flip;
    Vector3 originalScale;
    Vector3 targetScale;
    
    private void Awake()
    {
        flip = false;

        tr = transform;
        originalScale = tr.localScale;
        targetScale = originalScale* TargetScale;

        tr.localScale = new Vector3(originalScale.x, targetScale.y, originalScale.z);
    }

    private void FixedUpdate()
    {
        tr.localPosition += Vector3.right * 2f * Time.fixedDeltaTime *moveSpeed;

        if (!flip)
        {
            tr.localScale += Vector3.right * Time.fixedDeltaTime * scaleSpeed;
            tr.localScale -= Vector3.up * Time.fixedDeltaTime * scaleSpeed;
            if (tr.localScale.x > targetScale.x )
                flip = true;
        }
        else if (flip)
        {
            tr.localScale += Vector3.right * -1 * Time.fixedDeltaTime * scaleSpeed;
            tr.localScale -= Vector3.up * -1 * Time.fixedDeltaTime * scaleSpeed;
            if (tr.localScale.x < originalScale.x)
                flip = false;
        }
            

        if(tr.localPosition.x > 20)
            tr.localPosition += Vector3.right * -40;
    }
}
