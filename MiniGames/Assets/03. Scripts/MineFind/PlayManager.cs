using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStatus
{
    MainMenu,
    PrepareToPlay,
    OnPlay,
    Gameover
}
public class PlayManager : MonoBehaviour
{
    public static PlayManager instance;
    public GameStatus gameStatus;

    private void Awake()
    {
        if(instance != null)
            Destroy(instance);
        instance = this;

        StartCoroutine(FirstSetup());
    }

    public void ChangeGameStatusMainMenu()
    {
        ChangeGameStatus(GameStatus.MainMenu);
    }
    public void ChangeGameStatusPrepareToPlay()
    {
        ChangeGameStatus(GameStatus.PrepareToPlay);
    }
    public void ChangeGameStatusOnPlay()
    {
        ChangeGameStatus(GameStatus.OnPlay);
    }
    public void ChangeGameStatusGameOver()
    {
        ChangeGameStatus(GameStatus.Gameover);
    }

    IEnumerator FirstSetup()
    {
        yield return new WaitUntil(() => GUImain.instance != null);

        ChangeGameStatusMainMenu();
        yield return null;
    }
    void ChangeGameStatus(GameStatus inputGameStatus)
    {
        gameStatus = inputGameStatus;

        GUImain.instance.PanelSetup();

        switch (gameStatus)
        {
            case GameStatus.MainMenu:
                {
                    GUImain.instance.PanelSetup();

                    if (StageManager.instance != null)
                        StageManager.instance.DestroyTiles();

                    return;
                }
            case GameStatus.PrepareToPlay:
                {
                    GUImain.instance.PanelSetup();
                    StageManager.instance.GenerateTiles();
                    ChangeGameStatusOnPlay();
                    return;
                }

            case GameStatus.OnPlay:
                {
                    GUImain.instance.PanelSetup();
                    return;
                }

            case GameStatus.Gameover:
                {
                    GUImain.instance.PanelSetup();
                    return;
                }

            default: return;
        }
    }
}
