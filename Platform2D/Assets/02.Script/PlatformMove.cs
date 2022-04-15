using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public enum MoveType
    {
        Horizontal,
        Vertical,
        UpDiagonal,
        DownDiagonal,
    }
    public MoveType movingType;
    public float startPosition;
    public float moveSpeed;
    public Vector2 moveVector;
    public float stayTime;

    private float stayTimer;
    private enum State {Awake,MoveForward,MoveBack};
    private GameObject moveArea;
    private GameObject renderObject;
    private Transform platform;
    private Transform moveZone;
    private Vector2 moveDir = new Vector2();
    private Vector2 start = new Vector2();
    private Vector2 end = new Vector2();

    private State state;
    private Vector2 leftUpper;
    private Vector2 leftUnder;
    private Vector2 rightUpper;
    private Vector2 rightUnder;


    private void OnDrawGizmos()
    {
        renderObject = transform.Find("Renderer").gameObject;
        moveArea = transform.Find("MoveArea").gameObject;

        platform = renderObject.GetComponent<Transform>();
        moveZone = moveArea.GetComponent<Transform>();

        leftUpper = new Vector2(moveZone.position.x - moveZone.localScale.x / 2 + platform.localScale.x / 2,
                                moveZone.position.y + moveZone.localScale.y / 2 - platform.localScale.y / 2);
        leftUnder = new Vector2(moveZone.position.x - moveZone.localScale.x / 2 + platform.localScale.x / 2,
                                moveZone.position.y - moveZone.localScale.y / 2 + platform.localScale.y / 2);
        rightUpper = new Vector2(moveZone.position.x + moveZone.localScale.x / 2 - platform.localScale.x / 2,
                                 moveZone.position.y + moveZone.localScale.y / 2 - platform.localScale.y / 2);
        rightUnder = new Vector2(moveZone.position.x + moveZone.localScale.x / 2 - platform.localScale.x / 2,
                                 moveZone.position.y - moveZone.localScale.y / 2 + platform.localScale.y / 2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(moveZone.position, moveZone.localScale);
        Gizmos.color = Color.red;
        switch (movingType)
        {
            case MoveType.Horizontal:
                start = leftUpper;
                end = rightUpper;
                Gizmos.DrawWireCube(start, platform.localScale);
                Gizmos.DrawWireCube(end, platform.localScale);
                Gizmos.DrawLine(start,end);
                break;
            case MoveType.Vertical:
                start = leftUnder;
                end = leftUpper;
                Gizmos.DrawWireCube(start, platform.localScale);
                Gizmos.DrawWireCube(end, platform.localScale);
                Gizmos.DrawLine(start, end);
                break;
            case MoveType.UpDiagonal:
                start = leftUnder;
                end = rightUpper;
                Gizmos.DrawWireCube(start, platform.localScale);
                Gizmos.DrawWireCube(end, platform.localScale);
                Gizmos.DrawLine(start, end);
                break;
            case MoveType.DownDiagonal:
                start = leftUpper;
                end = rightUnder;
                Gizmos.DrawWireCube(start, platform.localScale);
                Gizmos.DrawWireCube(end, platform.localScale);
                Gizmos.DrawLine(start, end);
                break;
            default:
                break;
        }
    }

    private void Awake()
    {
        stayTimer = stayTime;
        renderObject = transform.Find("Renderer").gameObject;
        moveArea = transform.Find("MoveArea").gameObject;
        renderObject.SetActive(true);
        moveArea.SetActive(false);      
        
        platform = renderObject.GetComponent<Transform>();
        moveZone = moveArea.GetComponent<Transform>();

        leftUpper = new Vector2(moveZone.position.x - moveZone.localScale.x / 2 + platform.localScale.x / 2,
                                moveZone.position.y + moveZone.localScale.y / 2 - platform.localScale.y / 2);
        leftUnder = new Vector2(moveZone.position.x - moveZone.localScale.x / 2 + platform.localScale.x / 2,
                                moveZone.position.y - moveZone.localScale.y / 2 + platform.localScale.y / 2);
        rightUpper = new Vector2(moveZone.position.x + moveZone.localScale.x / 2 - platform.localScale.x / 2,
                                 moveZone.position.y + moveZone.localScale.y / 2 - platform.localScale.y / 2);
        rightUnder = new Vector2(moveZone.position.x + moveZone.localScale.x / 2 - platform.localScale.x / 2,
                                 moveZone.position.y - moveZone.localScale.y / 2 + platform.localScale.y / 2);

    }

    private void FixedUpdate()
    {
        MovePlatform();        
    }
    public void MovePlatform()
    {
        switch (movingType)
        {
            case MoveType.Horizontal:
                MoveHorizontal();
                break;
            case MoveType.Vertical:
                MoveVertical();
                break;
            case MoveType.UpDiagonal:
                MoveUpDiagonal();
                break;
            case MoveType.DownDiagonal:
                MoveDownDiagonal();
                break;
            default:
                break;
        }
    }
    public void MoveHorizontal()
    {
        switch (state)
        {
            case State.Awake:

                start = leftUpper;
                end = rightUpper;

                moveDir = Vector2.right;
                platform.position = start + ((end - start) * startPosition);

                state++;
                break;

            case State.MoveForward:
                moveVector = moveDir * moveSpeed * Time.fixedDeltaTime;
                platform.position += (Vector3)moveVector;

                if (platform.position.x >= end.x)
                {
                    moveVector = Vector2.zero;
                    platform.position = end;
                    stayTimer -= Time.fixedDeltaTime;

                    if (stayTimer <= 0)
                    {
                        stayTimer = stayTime;
                        state++;
                    }
                }
                break;

            case State.MoveBack:
                moveVector = moveDir * moveSpeed * Time.fixedDeltaTime * -1;
                platform.position += (Vector3)moveVector;
                if (platform.position.x <= start.x)
                {
                    moveVector = Vector2.zero;
                    platform.position = start;
                    stayTimer -= Time.fixedDeltaTime;

                    if (stayTimer <= 0)
                    {
                        stayTimer = stayTime;
                        state--;
                    }
                }
                break;

            default:
                break;
        }
    }
    public void MoveVertical()
    {
        switch (state)
        {
            case State.Awake:
                start = leftUnder;
                end = leftUpper;

                moveDir = Vector2.up;
                platform.position = start + ((end - start) * startPosition);

                state++;
                break;

            case State.MoveForward:
                moveVector = moveDir * moveSpeed * Time.fixedDeltaTime;
                platform.position += (Vector3)moveVector;

                if (platform.position.y >= end.y)
                {
                    moveVector = Vector2.zero;
                    platform.position = end;
                    stayTimer -= Time.fixedDeltaTime;

                    if (stayTimer <= 0)
                    {
                        stayTimer = stayTime;
                        state++;
                    }
                }

                break;

            case State.MoveBack:
                moveVector = moveDir * moveSpeed * Time.fixedDeltaTime * -1;
                platform.position += (Vector3)moveVector;

                if (platform.position.y <= start.y)
                {
                    moveVector = Vector2.zero;
                    platform.position = start;
                    stayTimer -= Time.fixedDeltaTime;

                    if (stayTimer <= 0)
                    {
                        stayTimer = stayTime;
                        state--;
                    }
                }

                break;

            default:
                break;
        }
    }
    public void MoveUpDiagonal()
    {
        switch (state)
        {
            case State.Awake:

                start = leftUnder;
                end = rightUpper;

                moveDir = (rightUpper-(Vector2)moveZone.position).normalized;
                platform.position = start + ((end - start) * startPosition);

                state++;
                break;

            case State.MoveForward:
                
                moveVector = moveDir * moveSpeed * Time.fixedDeltaTime;
                platform.position += (Vector3)moveVector;

                if (platform.position.x >= end.x)
                {
                    moveVector = Vector2.zero;
                    platform.position = end;
                    stayTimer -= Time.fixedDeltaTime;

                    if (stayTimer <= 0)
                    {
                        stayTimer = stayTime;
                        state++;
                    }        
                }

                break;

            case State.MoveBack:
                
                moveVector = moveDir * moveSpeed * Time.fixedDeltaTime * -1;
                platform.position += (Vector3)moveVector;

                if (platform.position.x <= start.x)
                {
                    moveVector = Vector2.zero;
                    platform.position = start;
                    stayTimer -= Time.fixedDeltaTime;

                    if (stayTimer <= 0)
                    {
                        stayTimer = stayTime;
                        state--;
                    }
                }

                break;

            default:
                break;
        }
    }
    public void MoveDownDiagonal()
    {
        switch (state)
        {
            case State.Awake:

                start = leftUpper;
                end = rightUnder;

                moveDir = (rightUnder - (Vector2)moveZone.position).normalized;
                platform.position = start + ((end - start) * startPosition);


                state++;
                break;

            case State.MoveForward:
                moveVector = moveDir * moveSpeed * Time.fixedDeltaTime;
                platform.position += (Vector3)moveVector;


                if (platform.position.x >= end.x)
                {
                    moveVector = Vector2.zero;
                    platform.position = end;
                    stayTimer -= Time.fixedDeltaTime;

                    if (stayTimer <= 0)
                    {
                        stayTimer = stayTime;
                        state++;
                    }
                }
                break;

            case State.MoveBack:
                moveVector = moveDir * moveSpeed * Time.fixedDeltaTime * -1;
                platform.position += (Vector3)moveVector;

                if (platform.position.x <= start.x)
                {
                    moveVector = Vector2.zero;
                    platform.position = start;
                    stayTimer -= Time.fixedDeltaTime;

                    if (stayTimer <= 0)
                    {
                        stayTimer = stayTime;
                        state--;
                    }
                }
                break;

            default:
                break;
        }
    }
}
