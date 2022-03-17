using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    public bool GameStarted;

    [SerializeField] private LevelList levelList;
    #endregion

    #region Singleton
    private static GameManager instance;
    public static GameManager Instance => instance ??= FindObjectOfType<GameManager>();
    #endregion

    void Start()
    {
        LevelManager.SetLevelManager(levelList);
    }
    public void StartGame()
    {
        UIManager.Instance.StartGame();
    }
    public void SwipeToStart()
    {
        GameStarted = true;
        UIManager.Instance.SwipeToStart();
    }
    public void OnLevelEnd(bool isWon)
    {
        GameStarted = false;
        if (isWon)
        {
            UIManager.Instance.OnLevelEnd(true);
        }
        else
        {
            UIManager.Instance.OnLevelEnd(false);
        }
    }

    public void NextGame()
    {
        LevelManager.NextLevel(); 
        UIManager.Instance.NextLevel();
        PlayerController.Instance.ResetPlayer();
    }

    public void RestartGame()
    {
        LevelManager.RestartLevel();

        GameStarted = true;
        UIManager.Instance.RestartLevel();
        PlayerController.Instance.ResetPlayer();
    }

    public bool IsGameStarted()
    {
        return GameStarted;
    }
}
