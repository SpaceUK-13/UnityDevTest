using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneUIManager : GameSceneController
{
    [SerializeField] Canvas gameUICanvas;
    [SerializeField] EndOfGamePopUp endOfGamePopUp;
    GameManager gameManager;

    public override void LoadDependencies(GameManager manager)
    {
        gameManager = manager;
        SetupUI();
    }

    private void SetupUI()
    {
        endOfGamePopUp.SetUpEndOfGamePopUp(null);
        gameManager.EndGameSession += endOfGamePopUp.OpenPopUp;
    }
}
