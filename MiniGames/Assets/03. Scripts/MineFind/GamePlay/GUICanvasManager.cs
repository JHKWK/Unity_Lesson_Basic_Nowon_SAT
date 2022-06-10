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
    public Slider difficultySlider;
    public Text difficultyText;
    public int difficultyMinValue;
    public int difficultyMaxValue;
    public int DifficultyDefaultValue;

    public Transform StartGameButton;

    [Header("OnPlayPannel")]
    [SerializeField] GameObject OnPlayPannel;
    public Text remainMinesCountText;
    public Text TotalScore;
    public Text openedTilesText;
    public Button winButton;
    public Transform heartsContainer;
    public List<GameObject> hearts;

    [Header("PuasePannel")]
    [SerializeField] GameObject PuasePanel;

    [Header("GamevoerPannel")]
    [SerializeField] GameObject GamevoerPannel;

    [Header("WinPannel")]
    [SerializeField] GameObject WinPannel;

    [Header("EndingPannel")]
    [SerializeField] GameObject EndingPannel;

    [Header("PrepareToPlayPannel")]
    [SerializeField] GameObject PrepareToPlayPannel;

    private void Awake()
    {
        if(instance!=null)
            Destroy(instance);

        instance = this;
    }
    public void GenerateHearts(int a,SkinInfo skininfo)
    {
        if(hearts.Count != 0)
        {
            for (int i = 0; i < hearts.Count; i++)
                Destroy(hearts[i]);
        }
        hearts = new List<GameObject>();
        for (int i = 0; i < a; i++)
        {
            GameObject go = Instantiate(skininfo.lifePrefab, heartsContainer);
            hearts.Add(go);
        }        
    }
    public void UpdateHearts(int a)
    {
        foreach (var heart in hearts)
        {
            heart.SetActive(false);
        }

        for (int i = 0; i < a; i++)
        {
            hearts[i].SetActive(true);
        }
    }
    void AllPanelUnactive()
    {
        TitlePannel.SetActive(false);
        MainMenuPannel.SetActive(false);
        OnPlayPannel.SetActive(false);
        PuasePanel.SetActive(false);
        GamevoerPannel.SetActive(false);
        WinPannel.SetActive(false);
        PrepareToPlayPannel.SetActive(false);
        EndingPannel.SetActive(false);
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
                AllPanelUnactive();
                TitlePannel.SetActive(true);
                break;

            case GameStatus.MainMenu:
                AllPanelUnactive();
                MainMenuPannel.SetActive(true);
                StartGameButton.gameObject.SetActive(false);
                break;

            case GameStatus.PrepareToPlay:
                AllPanelUnactive();
                PrepareToPlayPannel.SetActive(true);
                break;

            case GameStatus.OnPlay:
                AllPanelUnactive();
                OnPlayPannel.SetActive(true);
                break;

            case GameStatus.PausePlay:
                AllPanelUnactive();
                MainMenuPannel.SetActive(true);
                PuasePanel.SetActive(true);
                //Stage 선택하면 스타트버튼 활성화
                StartGameButton.gameObject.SetActive(false);
                break;

            case GameStatus.Gameover:
                AllPanelUnactive();
                OnPlayPannel.SetActive(true);
                PuasePanel.SetActive(true);
                GamevoerPannel.SetActive(true);
                break;

            case GameStatus.Win:
                AllPanelUnactive();

                if (StageManager.instance.isLastStage)
                {
                    EndingPannel.SetActive(true);
                }
                else
                {
                    OnPlayPannel.SetActive(true);
                    WinPannel.SetActive(true);
                }
                break;

            default:break;
        }
    }
}
