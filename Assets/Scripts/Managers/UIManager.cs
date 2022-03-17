using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    #region Instance
    private static UIManager instance;
    public static UIManager Instance => instance ??= FindObjectOfType<UIManager>();
    #endregion

    #region Variables
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private GameObject failPanel;
    [SerializeField] private GameObject tutorialPanel;//

    [SerializeField] private Text currentLevel;
    [SerializeField] private Text nextLevel;

    [SerializeField] private Image fillingBar;

    [SerializeField] private Color barColor;
    #endregion

    public void StartGame()
    {
        startPanel.SetActive(false);
        tutorialPanel.SetActive(true);
    }
    public void SwipeToStart()
    {
        tutorialPanel.SetActive(false);
        hudPanel.SetActive(true);
        TypeCurrentLevel();
    }

    public void OnLevelEnd(bool isWon)
    {
        hudPanel.SetActive(false);
        if (isWon)
        {
            winPanel.SetActive(true);
        }
        else
        {
            failPanel.SetActive(true);
        }
    }

    public void NextLevel()
    {
        winPanel.SetActive(false);
        tutorialPanel.SetActive(true);
        ResetBar();
        TypeCurrentLevel();
    }

    public void RestartLevel()
    {
        failPanel.SetActive(false);
        tutorialPanel.SetActive(true);
        ResetBar();
        TypeCurrentLevel();
    }

    public void SetBar()
    {
        fillingBar.color = barColor;
    }

    private void ResetBar()
    {
        fillingBar.color = Color.white;
    }

    private void TypeCurrentLevel()
    {
        currentLevel.text = (LevelManager.GetCurrentLevelIndex() + 1).ToString();
        nextLevel.text = (LevelManager.GetCurrentLevelIndex() + 2).ToString();
    }
}
