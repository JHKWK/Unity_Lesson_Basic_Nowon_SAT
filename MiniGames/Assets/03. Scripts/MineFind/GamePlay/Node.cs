using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public enum NodeType { Num, Mine }
public class Node : MonoBehaviour
{
    public NodeType nodeType;
    [Header("��� �������")]
    [SerializeField] Text text;
    [SerializeField] Transform flag;
    [SerializeField] Transform mine;
    [SerializeField] LayerMask layerMask;
    [Header("���׸���")]
    [SerializeField] Material material_opened;
    [SerializeField] Material material_unOpen;
    [Header("������")]
    [SerializeField] Renderer _renderer;
    [Header("ȿ��")]
    [SerializeField] ParticleSystem EffectExploding;
    [SerializeField] ParticleSystem EffectPop;
    [Header("number �÷���")]
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
        //���� �ν��Ͻ� �ʱ�ȭ
        arounds.Clear();
        yield return new WaitUntil( () => arounds.Count==0 );

        // �ּ��Ҵ�
        _renderer = transform.Find("Renderer").GetComponent<Renderer>();

        //��� �ʱ�ȭ(Ȱ��)
        EffectPop.gameObject.SetActive(true);
        EffectExploding.gameObject.SetActive(true);

        //��� �ʱ�ȭ(��Ȱ��)
        EffectExploding.Stop();
        EffectPop.Stop();
        isOpened = false;
        isFlaged = false;
        isSetDone = false;
        flag.gameObject.SetActive(false);
        mine.gameObject.SetActive(false);
        text.enabled = false;

        //�ֺ�Ÿ���ν�
        if (nodeType == NodeType.Num)
        {
            yield return new WaitUntil(()=>DetectAroundTiles());
            Debug.Log("Node.DetectAroundTiles Done");
        }            

        // Number/Mine �¾�
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
    //�ֺ�Ÿ���� ��߰����� ���ڰ����� ������ ���Ͽ� bool��ȯ
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
                //�ֺ�Ÿ�� ���ÿ���-��Ŭ��
                if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
                {
                    // ���ÿ��� ���� => �ֺ�Ÿ�Ͽ���
                    if (CheckMassiveOpenPossible())
                    {
                        foreach (var sub in arounds)
                        {
                            if (sub.isOpened == false)
                                sub.OpenTiles();
                        }
                    }
                    // ���ÿ��� �Ұ��� => �ֺ�Ÿ�ϸ�ŷ
                    else if (CheckMassiveOpenPossible() == false)
                    {
                        foreach (var sub in arounds)
                        {
                            if (sub.isOpened == false && sub.isFlaged == false)
                                sub._renderer.material.color = Color.grey;
                        }
                    }
                }
                // ���ÿ��� �Ұ��� => �ֺ�Ÿ�ϸ�ŷ ����
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
                // Ÿ�Ͽ���-��Ŭ��
                if (Input.GetMouseButtonUp(0))
                {
                    Debug.Log("��Ŭ��");
                    OpenTiles();
                }
                // ������-��Ŭ��
                if (Input.GetMouseButtonUp(1))
                {
                    Debug.Log("��Ŭ��");
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
            //�ֺ�Ÿ�ϸ�ŷ ����
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
