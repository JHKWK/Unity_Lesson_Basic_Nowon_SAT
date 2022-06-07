using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUICanvasManager : MonoBehaviour
{
    public static GUICanvasManager instance;

    [SerializeField] GameObject GamevoerPannel;

    [SerializeField] GameObject MainMenuPannel;

    [SerializeField] GameObject OnPlayPannel;
    public Text remainMinesCountText;
    public Text openedTilesText;
    public Button winButton;

    private void Awake()
    {
        if(instance!=null)
            Destroy(instance);

        instance = this;
    }
    public void PanelSetup()
    {
        switch (PlayManager.instance.gameStatus)
        {
            case GameStatus.MainMenu:
                MainMenuPannel.SetActive(true);
                OnPlayPannel.SetActive(false);
                GamevoerPannel.SetActive(false);
                break;

            case GameStatus.PrepareToPlay:
                break;

            case GameStatus.OnPlay:
                MainMenuPannel.SetActive(false);
                OnPlayPannel.SetActive(true);
                GamevoerPannel.SetActive(false);
                break;

            case GameStatus.PausePlay:
                break;


            case GameStatus.Gameover:
                MainMenuPannel.SetActive(false);
                OnPlayPannel.SetActive(false);
                GamevoerPannel.SetActive(true);
                break;

            case GameStatus.Win:
                break;

            default:break;
        }
    }

}
