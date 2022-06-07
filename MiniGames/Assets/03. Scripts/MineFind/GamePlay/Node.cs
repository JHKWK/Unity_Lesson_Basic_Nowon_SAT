using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public enum NodeType { Num, Mine }
public class Node : MonoBehaviour
{
    public NodeType nodeType;
    [Header("노드 구성요소")]
    [SerializeField] Text text;
    [SerializeField] Transform flag;
    [SerializeField] Transform mine;
    [SerializeField] LayerMask layerMask;
    [Header("머테리얼")]
    [SerializeField] Material material_opened;
    [SerializeField] Material material_unOpen;
    [Header("렌더러")]
    [SerializeField] Renderer _renderer;
    [Header("효과")]
    [SerializeField] ParticleSystem EffectExploding;
    [SerializeField] ParticleSystem EffectPop;
    [Header("number 컬러셋")]
    [SerializeField] Color color1;
    [SerializeField] Color color2;
    [SerializeField] Color color3;
    [SerializeField] Color color4;
    [SerializeField] Color color5;
    [SerializeField] Color color6;
    [SerializeField] Color color7;
    [SerializeField] Color color8;

    public int num;
    public bool isOpened;
    public float EffectIntervalTime;
    public bool isSetDone;
    public bool isFlaged 
    {
        get { return _isFlaged; }
        set 
        {
            if ( isOpened == false )
            {
                if (value == true)
                {
                    flag.gameObject.SetActive(true);
                }
                    
                if (value == false)
                {
                    flag.gameObject.SetActive(false);
                }
            }
            _isFlaged = value; 
        }
    }

    bool _isFlaged;
    [SerializeField] List<Node> arounds = new List<Node>();

    private void Awake()
    {
        nodeType = NodeType.Num;
    }

    public void Setup()
    {
         StartCoroutine(E_Setup());
    }

    IEnumerator E_Setup()
    {
        //기존 인스턴스 초기화
        arounds.Clear();
        yield return new WaitUntil( () => arounds.Count==0 );

        // 주소할당
        _renderer = transform.Find("Renderer").GetComponent<Renderer>();

        //요소 초기화(활성)
        EffectPop.gameObject.SetActive(true);
        EffectExploding.gameObject.SetActive(true);

        //요소 초기화(비활성)
        EffectExploding.Stop();
        EffectPop.Stop();
        isOpened = false;
        isFlaged = false;
        isSetDone = false;
        flag.gameObject.SetActive(false);
        mine.gameObject.SetActive(false);
        text.enabled = false;

        //주변타일인식
        if (nodeType == NodeType.Num)
        {
            yield return new WaitUntil(()=>DetectAroundTiles());
            Debug.Log("Node.DetectAroundTiles Done");
        }            

        // Number/Mine 셋업
        switch (nodeType)
        {
            case NodeType.Num:
                yield return new WaitUntil(() => SetNum());    
                Debug.Log("Node.SetNum Done");
                break;

            case NodeType.Mine:
                Debug.Log("Node.SetMIne Done");
                break;
        }

        isSetDone = true;
        StageManager.instance.TilesSetDone++;
        yield return null;
    }

    bool DetectAroundTiles()
    {
        GetComponent<Collider>().enabled = false;
        Collider[] cols = Physics.OverlapSphere(transform.position, 1, layerMask);
        foreach (var sub in cols)
        {
            if (sub.TryGetComponent<Node>(out Node node))
                arounds.Add(node);
        }
        GetComponent<Collider>().enabled = true;

        return true;
    }
    bool SetNum() 
    {
        num = 0;

        foreach (var sub in arounds)
        {
            if (sub.nodeType == NodeType.Mine)
                num++;
        }

        if (num == 0) text.enabled = false;
        if (num == 1) text.color = color1;
        if (num == 2) text.color = color2;
        if (num == 3) text.color = color3;
        if (num == 4) text.color = color4;
        if (num == 5) text.color = color5;
        if (num == 6) text.color = color6;
        if (num == 7) text.color = color7;
        if (num == 8) text.color = color8;

        text.text = num.ToString();

        return true;
    }
    void OpenTiles()
    {
        StartCoroutine(E_OpenTiles());
    }
    IEnumerator E_OpenTiles()
    {
        if (isFlaged == false)
        {
            isOpened = true;
            StageManager.instance.OpenedTilesCount++;

            if (num != 0)
                text.enabled = true;

            switch (nodeType)
            {
                case NodeType.Num:
                    if (num == 0)
                    {
                        _renderer.material = material_opened;
                        yield return new WaitForSeconds(EffectIntervalTime);

                        foreach (var sub in arounds)
                        {
                            if (sub.isOpened == false)
                                sub.OpenTiles();
                        }
                    }
                    else
                        _renderer.material = material_opened;
                    PlayEffect();
                    yield return new WaitWhile(() => EffectPop.isPlaying);
                    EffectPop.gameObject.SetActive(false);
                    break;

                case NodeType.Mine:
                    _renderer.material = material_opened;
                    mine.gameObject.SetActive(true);
                    PlayEffect();
                    yield return new WaitForSeconds(EffectIntervalTime * 10);

                    StageManager.instance.GameOver();
                    
                    break;

                default: break;
            }
        }
        yield return null;
    }
    void PlayEffect()
    {
        switch (nodeType)
        {
            case NodeType.Num:
                EffectPop.Play();
                break;

            case NodeType.Mine:
                EffectPop.Play();
                EffectExploding.Play();
                break;

            default:
                break;
        }
    }
    //주변타일의 깃발갯수와 지뢰갯수가 같은지 비교하여 bool반환
    bool CheckMassiveOpenPossible()
    {
        bool result = false;

        if(isOpened)
        {
            int arrounflagCount = 0;
            foreach (var sub in arounds)
            {
                if(sub.isFlaged == true)                
                    arrounflagCount++;                
            }

            if(arrounflagCount == num)
                result = true;
        }

        return result;
    }
    private void OnMouseOver()
    {
        if (PlayManager.instance.gameStatus == GameStatus.OnPlay)
        {
            if (isOpened == true)
            {
                //주변타일 동시오픈-양클릭
                if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
                {
                    // 동시오픈 가능 => 주변타일오픈
                    if (CheckMassiveOpenPossible())
                    {
                        foreach (var sub in arounds)
                        {
                            if (sub.isOpened == false)
                                sub.OpenTiles();
                        }
                    }
                    // 동시오픈 불가능 => 주변타일마킹
                    else if (CheckMassiveOpenPossible() == false)
                    {
                        foreach (var sub in arounds)
                        {
                            if (sub.isOpened == false && sub.isFlaged == false)
                                sub._renderer.material.color = Color.grey;
                        }
                    }
                }
                // 동시오픈 불가능 => 주변타일마킹 해제
                if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
                {
                    foreach (var sub in arounds)
                    {
                        if (sub.isOpened == false && sub.isFlaged == false)
                            sub._renderer.material = material_unOpen;
                    }
                }
            }
            else if (isOpened == false)
            {
                // 타일오픈-좌클릭
                if (Input.GetMouseButtonUp(0))
                {
                    Debug.Log("좌클릭");
                    OpenTiles();
                }
                // 깃발토글-우클릭
                if (Input.GetMouseButtonUp(1))
                {
                    Debug.Log("우클릭");
                    if (isFlaged == false)
                    {
                        isFlaged = true;
                        StageManager.instance.FlaggedTilesCount++;
                    }
                    else if (isFlaged)
                    {
                        isFlaged = false;
                        StageManager.instance.FlaggedTilesCount--;
                    }
                }
            }
        }
    }

    private void OnMouseExit()
    {
        if (PlayManager.instance.gameStatus == GameStatus.OnPlay)
        {
            //주변타일마킹 해제
            if (isOpened == true)
            {
                foreach (var sub in arounds)
                {
                    if (sub.isOpened == false && sub.isFlaged == false)
                        sub._renderer.material = material_unOpen;
                }
            }
        }
    }

}
