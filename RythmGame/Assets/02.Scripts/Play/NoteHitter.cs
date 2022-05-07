using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NoteHitter : MonoBehaviour
{
    [SerializeField] KeyCode keyCode;
    [SerializeField] HitJudgeInfo hitJudgeInfo;
    [SerializeField] LayerMask noteLayer;
    [SerializeField] Color pressdColor;
    SpriteRenderer icon;
    Color originalColor;
    private void Awake()
    {
        icon = GetComponent<SpriteRenderer>();
        originalColor = icon.color;
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode)) TryHitNote();
        if(Input.GetKey(keyCode)) ChangeColor();
        else RollbackColor();
    }
    private bool TryHitNote()
    {
        HitType hitType = HitType.Miss;
        List<Collider2D> overlaps =
            Physics2D.OverlapBoxAll(transform.position,
                                    new Vector2(transform.lossyScale.x / 2, hitJudgeInfo.hitJudgeHeigth_Miss),
                                    0, noteLayer).ToList(); //ToList() => System.Linq
        if (overlaps.Count > 0)
        {
            overlaps.OrderBy(x => Mathf.Abs(transform.position.y - x.transform.position.y));
            float distance = Mathf.Abs(transform.position.y - overlaps[0].transform.position.y);

            if (distance < hitJudgeInfo.hitJudgeHeigth_Perfect) hitType = HitType.Perfect;
            else if (distance < hitJudgeInfo.hitJudgeHeigth_Great) hitType = HitType.Great;
            else if (distance < hitJudgeInfo.hitJudgeHeigth_Good) hitType = HitType.Good;
            else if (distance < hitJudgeInfo.hitJudgeHeigth_Miss) hitType = HitType.Miss;
            else hitType = HitType.Bad;
                        
            overlaps[0].GetComponent<Note>().Hit(hitType);
            overlaps[0].gameObject.SetActive(false);

            return true;
        }
        return false;
    }
    void ChangeColor() => icon.color = pressdColor;
    void RollbackColor() => icon.color = originalColor;
    private void OnDrawGizmos()
    {
        //Bad 판정 범위 기즈모
        Gizmos.color = Color.gray;
        Gizmos.DrawWireCube(transform.position,
            new Vector3(transform.lossyScale.x / 2, hitJudgeInfo.hitJudgeHeigth_Bad, 0));

        //Miss 판정 범위 기즈모
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position,
            new Vector3(transform.lossyScale.x / 2, hitJudgeInfo.hitJudgeHeigth_Miss, 0));

        //Good 판정 범위 기즈모
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position,
            new Vector3(transform.lossyScale.x / 2, hitJudgeInfo.hitJudgeHeigth_Good, 0));

        //Great 판정 범위 기즈모
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position,
            new Vector3(transform.lossyScale.x / 2, hitJudgeInfo.hitJudgeHeigth_Great, 0));

        //Perfect 판정 범위 기즈모
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position,
            new Vector3(transform.lossyScale.x / 2, hitJudgeInfo.hitJudgeHeigth_Perfect, 0));
    }

}

