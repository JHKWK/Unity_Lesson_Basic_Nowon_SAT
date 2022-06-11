using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    public Animator playerAnimator;

    public bool GetBool(string name ) => playerAnimator.GetBool(name);

    public float GetFloat(string name) => playerAnimator.GetFloat(name);

    public float GetINT(string name) => playerAnimator.GetInteger(name);

    public void SetBool(string name, bool value) => playerAnimator.SetBool(name,value);

    public void SetFloat(string name, float value) => playerAnimator.SetFloat(name, value);

    public void SetINT(string name, int value) => playerAnimator.SetInteger(name,value);

    public void SetTrigger(string name) => playerAnimator.SetTrigger(name);

    public bool IsClipPlaying(string name)
    {
        var stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(name) ? true : false;
    }

    public float GetClipTime(string name)
    {
        RuntimeAnimatorController ac = playerAnimator.runtimeAnimatorController;

        for (int i = 0; i < ac.animationClips.Length; i++)
        {
            if (ac.animationClips[i].name == name)
                return ac.animationClips[i].length;
        }
        return -1.0f;
    }
}
