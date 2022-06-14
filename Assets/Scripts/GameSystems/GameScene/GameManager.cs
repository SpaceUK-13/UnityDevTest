using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ICameraComponent
{
    [SerializeField] GameDataController dataLoader;
    [SerializeField] List<GameSceneController> gameSceneControllers;
    [SerializeField] Camera sceneCamera;
    public Action<GameplayState> EndGameSession;
    public Action LoadMainMenuScene;
    public GameDataController GameData => dataLoader;

    private void Start()
    {
        EndGameSession += UpdatePlayerProgression;
        LoadDependencies();
    }

    private void LoadDependencies()
    {
        foreach (var controllers in gameSceneControllers)
        {
            controllers.LoadDependencies(this);
        }
    }

    public Camera GetSceneCamera()
    {
        return sceneCamera;
    }

    public GameSceneController GetGameControllerComponent(Type type)
    {
        foreach (var controller in gameSceneControllers)
        {
            if (type == controller.GetType())
                return controller;
        }

        return null;
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnApplicationQuit()
    {
        dataLoader.ClearOldData();
    }

    private void UpdatePlayerProgression(GameplayState gameplayState)
    {
        var progression = GameData.GetData(DataType.PlayerProgressionData) as PlayerProgressionData;
        progression.PlayersNumberOfMathces += 1;
        if (gameplayState == GameplayState.GameWon)
            progression.PlayersConsecutiveWins += 1;
        else if (gameplayState == GameplayState.GameLost)
            progression.PlayersConsecutiveWins = 0;

        GameData.SaveData(progression,DataType.PlayerProgressionData);
    }
}
