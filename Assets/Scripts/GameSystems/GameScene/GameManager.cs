using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, ICameraComponent
{
    [SerializeField] GameDataLoader dataLoader;
    [SerializeField] List<GameSceneController> gameSceneControllers;
    [SerializeField] Camera sceneCamera;
    public Action<GameplayState> EndGameSession;

    public GameDataLoader GameData => dataLoader;

    private void Start()
    {
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

    private void OnApplicationQuit()
    {
        dataLoader.ClearOldData();
    }
}
