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
        yield return new WaitUntil(() => GUICanvasManager.instance != null);
        yield return new WaitUntil(() => StageManager.instance != null);

        ChangeGameStatusMainMenu();

        yield return null;
    }
    void ChangeGameStatus(GameStatus inputGameStatus)
    {
        gameStatus = inputGameStatus;

        GUICanvasManager.instance.PanelSetup();

        switch (gameStatus)
        {
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
