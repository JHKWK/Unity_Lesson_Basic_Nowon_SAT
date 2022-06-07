using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUICanvasManager : MonoBehaviour
{
    public static GUICanvasManager instance;

    [Header("TitlePannel")]
    [SerializeField] GameObject TitlePannel;

    [Header("MainMenuPannel")]
    [SerializeField] GameObject MainMenuPannel;
    public Transform StartGameButton;

    [Header("OnPlayPannel")]
    [SerializeField] GameObject OnPlayPannel;
    public Text remainMinesCountText;
    public Text openedTilesText;
    public Button winButton;

    [Header("PuasePannel")]
    [SerializeField] GameObject PuasePanel;

    [Header("GamevoerPannel")]
    [SerializeField] GameObject GamevoerPannel;

    [Header("WinPannel")]
    [SerializeField] GameObject WinPannel;

    [Header("PrepareToPlayPannel")]
    [SerializeField] GameObject PrepareToPlayPannel;

    private void Awake()
    {
        if(instance!=null)
            Destroy(instance);

        instance = this;
    }
    public void activeStartButton()
    {
        StartGameButton.gameObject.SetActive(true);
    }
    public void PanelSetup()
    {
        switch (PlayManager.instance.gameStatus)
        {
            case GameStatus.Title:
                TitlePannel.SetActive(true);
                MainMenuPannel.SetActive(false);
                OnPlayPannel.SetActive(false);
                PuasePanel.SetActive(false);
                GamevoerPannel.SetActive(false);
                WinPannel.SetActive(false);
                PrepareToPlayPannel.SetActive(false);
                

                break;

            case GameStatus.MainMenu:
                TitlePannel.SetActive(false);
                MainMenuPannel.SetActive(true);
                OnPlayPannel.SetActive(false);
                PuasePanel.SetActive(false);
                GamevoerPannel.SetActive(false);
                WinPannel.SetActive(false);
                PrepareToPlayPannel.SetActive(false);

                StartGameButton.gameObject.SetActive(false);
                break;

            case GameStatus.PrepareToPlay:
                TitlePannel.SetActive(false);
                MainMenuPannel.SetActive(false);
                OnPlayPannel.SetActive(false);
                PuasePanel.SetActive(false);
                GamevoerPannel.SetActive(false);
                WinPannel.SetActive(false);
                PrepareToPlayPannel.SetActive(true);

                break;

            case GameStatus.OnPlay:
                TitlePannel.SetActive(false);
                MainMenuPannel.SetActive(false);
                OnPlayPannel.SetActive(true);
                PuasePanel.SetActive(false);
                GamevoerPannel.SetActive(false);
                WinPannel.SetActive(false);
                PrepareToPlayPannel.SetActive(false);

                break;

            case GameStatus.PausePlay:
                TitlePannel.SetActive(false);
                MainMenuPannel.SetActive(true);
                OnPlayPannel.SetActive(false);
                PuasePanel.SetActive(true);
                GamevoerPannel.SetActive(false);
                WinPannel.SetActive(false);
                PrepareToPlayPannel.SetActive(false);

                StartGameButton.gameObject.SetActive(false);
                break;


            case GameStatus.Gameover:
                TitlePannel.SetActive(false);
                MainMenuPannel.SetActive(false);
                OnPlayPannel.SetActive(false);
                PuasePanel.SetActive(true);
                GamevoerPannel.SetActive(true);
                WinPannel.SetActive(false);
                PrepareToPlayPannel.SetActive(false);

                break;

            case GameStatus.Win:
                TitlePannel.SetActive(false);
                MainMenuPannel.SetActive(false);
                OnPlayPannel.SetActive(false);
                PuasePanel.SetActive(false);
                GamevoerPannel.SetActive(false);
                WinPannel.SetActive(true);
                PrepareToPlayPannel.SetActive(false);
                break;

            default:break;
        }
    }

}
