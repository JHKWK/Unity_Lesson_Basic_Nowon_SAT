using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStatus
{
    Title,
    MainMenu,
    PrepareToPlay,
    OnPlay,
    PausePlay,
    Gameover,
    Win
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
    public void ChangeGameStatusTitle()
    {
        ChangeGameStatus(GameStatus.Title);
    }
    public void ChangeGameStatusMainMenu()
    {
        ChangeGameStatus(GameStatus.MainMenu);
    }
    public void ChangeGameStatusPrepareToPlay()
    {
        ChangeGameStatus(GameStatus.PrepareToPlay);
    }
    public void ChangeGameStatusPausePlay()
    {
        ChangeGameStatus(GameStatus.PausePlay);
    }
    public void ChangeGameStatusOnPlay()
    {
        ChangeGameStatus(GameStatus.OnPlay);
    }
    public void ChangeGameStatusGameOver()
    {
        ChangeGameStatus(GameStatus.Gameover);
    }
    public void ChangeGameStatusWin()
    {
        ChangeGameStatus(GameStatus.Win);
    }

    IEnumerator FirstSetup()
    {
        yield return new WaitUntil(() => GUICanvasManager.instance != null);
        ChangeGameStatus(GameStatus.Title);

        yield return new WaitUntil(() => StageManager.instance != null);

        yield return null;
    }
    void ChangeGameStatus(GameStatus inputGameStatus)
    {
        gameStatus = inputGameStatus;

        GUICanvasManager.instance.PanelSetup();

        switch (gameStatus)
        {
            case GameStatus.Title:
                GUICanvasManager.instance.PanelSetup();
                break;

            case GameStatus.MainMenu:
                GUICanvasManager.instance.PanelSetup();
                break;

            case GameStatus.PrepareToPlay:
                GUICanvasManager.instance.PanelSetup();
                StageManager.instance.GenerateTiles();
                break;

            case GameStatus.OnPlay:
                GUICanvasManager.instance.PanelSetup();
                break;

            case GameStatus.PausePlay:
                GUICanvasManager.instance.PanelSetup();
                break;

            case GameStatus.Gameover:
                GUICanvasManager.instance.PanelSetup();
                break;

            case GameStatus.Win:
                GUICanvasManager.instance.PanelSetup();
                break;

            default: break;
        }
    }



}
