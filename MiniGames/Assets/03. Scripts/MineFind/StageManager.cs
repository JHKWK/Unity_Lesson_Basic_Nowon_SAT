using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;
    public Transform node;
    public Transform tiles;

    public int randomSeed;

    Node[,] nodes;
    int width;
    int height;
    public float scale = 1;
    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;


    }
    public void SetWH(int w)
    {
        width = w / 100;
        height = w % 100;
    }

    public void GenerateTiles()
    {
        Debug.Log($"{width},{height}");
        transform.localScale = Vector3.one;
        tiles.transform.localPosition = Vector3.zero;

        DestroyTiles();

        nodes = new Node[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {

                Transform tile = Instantiate(node, new Vector3(i, j, 0), Quaternion.identity, tiles);
                nodes[i, j] = tile.GetComponent<Node>();

                int a = Random.Range(0, randomSeed);
                if (a == 0) nodes[i, j].nodeType = NodeType.Mine;
                else nodes[i, j].nodeType = NodeType.Num;
            }
        }

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Debug.Log(nodes[i, j].nodeType);
                nodes[i, j].Setup();
            }
        }

        ResetPosCenter();
    }
    public void DestroyTiles()
    {
        if (tiles.childCount > 0)
        {
            Node[] arrs = tiles.GetComponentsInChildren<Node>();
            foreach (var tile in arrs)
            {
                Destroy(tile.gameObject);
            }
        }    
    }
    void ResetPosCenter()

    {
        // max size : 28x14
        if (width > height * 2)
        {
            if (width > 28)
            {

                scale = 1f / (float)width * 28f;
            }
            else scale = 1f;
        }
        else if (width < height * 2f)
        {
            if (height > 14)
            {
                scale = 1 / (float)height * 14f;
            }
            else scale = 1f;
        }
        else scale = 1f;

        Debug.Log(scale);

        tiles.Translate(new Vector3(0.5f - (float)width * 0.5f, 0.5f - (float)height * 0.5f, 0));
        this.transform.localScale = Vector3.one * scale;
    }
}
