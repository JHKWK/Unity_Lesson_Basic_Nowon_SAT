using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public LayerMask groundLayer;
    public bool isDetected;
    public float heightOffset;
    public Collider2D groundDetector;
    public Collider2D lastGround;
    public Collider2D detectedGround;
    public Collider2D ignoringGround;


    private Rigidbody2D rb;
    private CapsuleCollider2D col;
    private Vector2 size;
    private Vector2 center;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        size.x = col.size.x * 0.25f;
        size.y = 0.005f;
    }
    private void Update()
    {
        center.x = rb.position.x + col.offset.x;
        center.y = rb.position.y + col.offset.y - col.size.y*0.5f - size.y -heightOffset;
        groundDetector = Physics2D.OverlapBox(center, size, 0, groundLayer);
        isDetected = groundDetector;

        if (detectedGround != null)
            lastGround = detectedGround;
    }
    private void OnDrawGizmos()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        size.x = col.size.x * 0.25f;
        size.y = 0.005f;

        center.x = rb.position.x + col.offset.x;
        center.y = rb.position.y + col.offset.y - col.size.y * 0.5f - size.y - heightOffset;

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(center.x,center.y,0),new Vector3(size.x,size.y,0));
    }

    public void IgnoreLastGroundUntilPassed()
    {
        ignoringGround = lastGround;
        if (ignoringGround !=null)
        StartCoroutine(E_IgnorGroundUntilPassedIt(lastGround));
    }
    IEnumerator E_IgnorGroundUntilPassedIt(Collider2D groundCol) 
    {
        Physics2D.IgnoreCollision(col,groundCol, true);
        float passingGoundColCenter = groundCol.transform.position.y + groundCol.offset.y;

        //�÷��̾ �׶��带 �������� �����ϴ��� üũ
        yield return new WaitUntil(()=> { return rb.position.y + col.offset.y - col.size.y / 2 < passingGoundColCenter; });

        //�÷��̾ �׶��带 ������ ����������
        yield return new WaitUntil(() => 
        {
            bool isPassed = false;
            if (groundCol != null)
            {
                //�����̵��� ����Ͽ� ��ġ ����
                passingGoundColCenter = groundCol.transform.position.y + groundCol.offset.y;

                //�÷��̾ ������ ��� �ߴ��� üũ
                //1. �÷��̾ �������鼭 �������
                //2. �÷��̾ �ö󰡸鼭 �������
                if ((rb.position.y + col.offset.y + col.size.y/2 < passingGoundColCenter - size.y) ||
                     (rb.position.y + col.offset.y + col.size.y / 2 > passingGoundColCenter + size.y))
                {
                    isPassed = true;
                }
            }
            // ������ ����������
            else isPassed = true;

            return isPassed;
        });
        Physics2D.IgnoreCollision(col, groundCol, false);
    }
}
