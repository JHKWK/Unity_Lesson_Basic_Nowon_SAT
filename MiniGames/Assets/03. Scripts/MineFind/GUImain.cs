using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUImain : MonoBehaviour
{
    public static GUImain instance;
    public GameObject GamevoerPannel;
    public GameObject MainMenuPannel;
    public GameObject OnPlayPannel;

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
                MainMenuPannel.SetActive(true);
                OnPlayPannel.SetActive(false);
                GamevoerPannel.SetActive(false);
                break;

            case GameStatus.OnPlay:
                MainMenuPannel.SetActive(false);
                OnPlayPannel.SetActive(true);
                GamevoerPannel.SetActive(false);
                break;

            case GameStatus.Gameover:
                MainMenuPannel.SetActive(false);
                OnPlayPannel.SetActive(false);
                GamevoerPannel.SetActive(true);
                break;
            
            default:
                break;
        }
    }

}
