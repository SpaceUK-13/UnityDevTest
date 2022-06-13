using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EndOfGamePopUp : MonoBehaviour
{
    [SerializeField] Transform popUpTab;
    [SerializeField] TextMeshProUGUI bannerMessage;
    [SerializeField] Button quitLevelButton;



    private void Start()
    {
        bannerMessage.text = string.Empty;
        popUpTab.gameObject.SetActive(false);
    }

    public void SetUpEndOfGamePopUp(Action quitLevelAction)
    {
     
        quitLevelButton.onClick.AddListener(()=>
        {
            quitLevelAction?.Invoke();
            popUpTab.gameObject.SetActive(false);
        });
    }

    public void OpenPopUp(GameplayState state)
    {
        if (state == GameplayState.EnemyTurn || state == GameplayState.PlayerTurn)
            return;

        var endMessage = state == GameplayState.GameLost ? "You Lost" : "You Won";
        popUpTab.gameObject.SetActive(true);
        bannerMessage.text = endMessage;
    }

}
