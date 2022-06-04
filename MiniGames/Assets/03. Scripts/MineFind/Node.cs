using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public enum NodeType { Num, Mine }
public class Node : MonoBehaviour
{
    public NodeType nodeType;

    [SerializeField] Collider[] hits;
    [SerializeField] List<Node> arounds;
    [SerializeField] Text text;
    [SerializeField] Transform flag;
    [SerializeField] LayerMask layerMask;

    [SerializeField] Material material_opened;
    [SerializeField] Material material_unOpen;

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
    public bool isFlaged 
    {
        get { return _isFlaged; }
        set 
        {
            if (!isOpened)
            {
                if (value == true)
                    flag.gameObject.SetActive(true);
                if (value == false)
                    flag.gameObject.SetActive(false);
            }

            _isFlaged = value; 
        }
    }

    bool _isFlaged;
    float scale;

    public void Setup()
    {
        arounds.Clear();
        scale = StageManager.instance.scale;
        isOpened = false;
        text.gameObject.SetActive(true);
        flag.gameObject.SetActive(false);
        num = 0;

        switch (nodeType)
        {
            case NodeType.Num:
                {
                    SetNum();
                    break;
                }

            case NodeType.Mine:
                {
                    text.text = "X";
                    text.color = Color.black;
                    break;
                }
        }
    }
    void DetectAroundTiles()
    {
        gameObject.GetComponent<Collider>().enabled = false;        
        Collider[] cols = Physics.OverlapSphere(transform.position, scale * 0.9f, layerMask);

        foreach (var sub in cols)
        {
            if (sub.TryGetComponent<Node>(out Node node))
                arounds.Add(node);
        }

        gameObject.GetComponent<Collider>().enabled = true;
    }
    void SetNum() 
    {
        DetectAroundTiles();


        foreach (var sub in arounds)
        {
            if (sub.nodeType == NodeType.Mine)
            {
                Debug.Log(sub.transform.position);
                num++;
            }
        }

        if (num == 0)
        {
            text.gameObject.SetActive(false);
            text.color = Color.gray;
        }
        if (num == 1) text.color = color1;
        if (num == 2) text.color = color2;
        if (num == 3) text.color = color3;
        if (num == 4) text.color = color4;
        if (num == 5) text.color = color5;
        if (num == 6) text.color = color6;
        if (num == 7) text.color = color7;
        if (num == 8) text.color = color8;

        text.text = num.ToString();
    }
    


    void OpenTile()
    {
        if (!isFlaged)
        {
            isOpened = true;
            if (num != 0) text.gameObject.SetActive(true);
            scale = StageManager.instance.scale;
            switch (nodeType)
            {
                case NodeType.Num:
                    if (num == 0)
                    {
                        GetComponentInChildren<Renderer>().material = material_opened;
                        DetectAroundTiles();
                        foreach (var sub in arounds)
                        {
                            if (!sub.isOpened) sub.OpenTile();
                        }
                    }
                    else
                    {
                        GetComponentInChildren<Renderer>().material = material_opened;                        
                    }
                    break;

                case NodeType.Mine:
                    GetComponentInChildren<Renderer>().material = material_opened;
                    GetComponentInChildren<Renderer>().material.color = Color.red;
                    text.gameObject.SetActive(true);
                    PlayManager.instance.ChangeGameStatusGameOver();
                    Debug.Log("GameOver");
                    break;

                default: return;
            }
        }
    }

    private void OnMouseOver()
    {
        {
            if (Input.GetMouseButtonUp(0))
            {
                OpenTile();
            }

            if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
            {
                DetectAroundTiles();
                foreach (var sub in arounds)
                {
                    if (sub.isOpened == false)
                        sub.OpenTile();
                }
            }

            if (Input.GetMouseButtonUp(1))
            {
                if (!isFlaged)
                {
                    isFlaged = true;
                }
                else if (isFlaged)
                {
                    isFlaged = false;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        scale = StageManager.instance.scale;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, scale * 0.9f);
    }
}
