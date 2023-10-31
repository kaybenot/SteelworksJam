using System;
using UnityEngine;

public enum GameState
{
    CutScene,
    Searching,
    BossFight,
    GhostFollow,
    LastBoss,
    Pause,
    LevelFinished
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState state;
    public static event Action<GameState> OnGameStateChanged;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
            
    }
    void Start()
    {
        RenderSettings.fogDensity = 0.08f;
        CursorManager.HideCursor();
        //UpdateGameState(GameState.CutScene);
    }
    //public void UpdateGameState(GameState newState)
    //{
    //    state = newState;

    //    switch (newState)
    //    {
    //        case GameState.CutScene:
    //            //HandleMainMenu();
    //            break;
    //        case GameState.Searching:
    //           // HandleGameState();
    //            break;
    //        case GameState.BossFight:
    //            //HandleMainMenu();
    //            break;
    //        case GameState.GhostFollow:
    //            // HandleGameState();
    //            break;
    //        case GameState.LastBoss:
    //            //HandleMainMenu();
    //            break;
    //        case GameState.Pause:
    //           // HandlePauseState();
    //            break;
    //        case GameState.LevelFinished:
    //           //LevelFinishedState();
    //            break;
    //        default:
    //            throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
    //    }
    //    OnGameStateChanged(newState);
    //}
    public void ExitGame()
    {
        Application.Quit();
    }
}
