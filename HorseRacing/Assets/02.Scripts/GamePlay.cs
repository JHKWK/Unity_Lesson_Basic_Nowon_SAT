using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    static public GamePlay instance; //오브젝트에 직접 접근하기 편하게 하기위함

    public List<GameObject> players = new List<GameObject>();
    public List<Transform> finishedPlayers = new List<Transform>();
    public List<Transform> platforms = new List<Transform>();

    public Transform goal;
    public bool onPlay = false;
    private float playersStartZPos;
    private int totalPlayers;

    private void Awake()
    {
        instance = this; //오브젝트에 직접 접근하기 편하게 하기위함
    }

    void Start()
    {
        totalPlayers = players.Count;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (onPlay)
        {
            CheckPlayerReachToGoalAndStopMove();
            CheckGameIsFinished();
        }

        

    }

    public void PlayGame()
    {
        onPlay = true;
        playersStartZPos = players[0].transform.position.z;//[transformPosition.z];
        foreach (var sub in players)
        {
            Playermove playerMove = sub.GetComponent<Playermove>();
            if (playerMove != null)
                playerMove.doMove = true;
        }
    }

    public void CheckPlayerReachToGoalAndStopMove()
    {
        for (int i = players.Count -1; i > -1 ; i--) // for문 역순으로 돌리기
        {
            Playermove playermove = players[i].GetComponent<Playermove>();
            if (playermove.distance > goal.position.z - playersStartZPos)
            {
                playermove.doMove = false;
                finishedPlayers.Add(players[i].transform);
                players.Remove(players[i]);                
            }

        }
    }

    void CheckGameIsFinished()
    {
        if (finishedPlayers.Count >= totalPlayers)
        {
            onPlay = false;
            for (int i = 0; i < platforms.Count; i++)
            {
                finishedPlayers[i].position = platforms[i].position;
                // finishedPlayers[i].position = platforms[i].Find("Playerposition").position + new Vector3(0, finishedPlayers[i].lossyScale.y, 0);
                finishedPlayers[i].rotation = platforms[i].rotation;
                finishedPlayers[i].localScale = platforms[i].localScale;                

            }
        }
        
    }
}
