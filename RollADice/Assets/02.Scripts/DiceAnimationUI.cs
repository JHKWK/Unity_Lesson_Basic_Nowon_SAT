using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceAnimationUI : MonoBehaviour
{
    public static DiceAnimationUI Instance;

    public Image diceAnimationImage;
    public float diceAnimationTime;
    private Sprite[] Sprites;
    public delegate void AnimationFinishideEvent(int diceValue);


    private void Start()
    {
       Sprites =Resources.LoadAll<Sprite>("DiceVectorDark");
        
    }

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator E_DiceAnimation(int diceValue, DicePlayManager manager, AnimationFinishideEvent finishEvent)
    {
        
        float elapsedTime = 0;
        while (elapsedTime < diceAnimationTime)
        {
            elapsedTime += diceAnimationTime / 10;
            int tmpIdx = Random.Range(0,Sprites.Length);
            diceAnimationImage.sprite = Sprites[tmpIdx];
            yield return new WaitForSeconds(diceAnimationTime/10);
        }
        diceAnimationImage.sprite = Sprites[diceValue - 1];

        if (finishEvent != null)
            finishEvent(diceValue);
        yield return null;
        manager.animationCoroutine = null;
    }
}
