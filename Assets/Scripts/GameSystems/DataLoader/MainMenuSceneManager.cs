using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneManager : MonoBehaviour
{

    [SerializeField] GameDataLoader dataLoader;
    [SerializeField] MainMenuUIManager uiManager;
    [SerializeField] HeroManager heroManager;

    public Action HeroesReady;
    public Action<HeroSelectionIcon, HeroScriptableObject, bool> HeroSelected;
    public Action<bool> HeroSelectionReady;
    public Action EnterBattle;

    private void Awake()
    {

        if (!dataLoader.DataIsReady)
        {
            dataLoader.LoadingDone += LoadingComplete;
            dataLoader.Init();
        }
        else
        {
            LoadingComplete();
        }
        HeroSelected += HeroIsSelected;
        EnterBattle += LoadGameplayScene;
    }

    private void LoadingComplete()
    {
        uiManager.Init(dataLoader, this);
        heroManager.Init(dataLoader, this);
    }
    private void HeroIsSelected(HeroSelectionIcon icon, HeroScriptableObject hero, bool isSelected)
    {
        if (dataLoader.SelectedHeros.Count >= 3 && !dataLoader.AvailableHeros.Contains(hero))
        {
            icon.ForceDeselect();
            return;
        }

     
        dataLoader.ToogleSelectedHeroes(isSelected, hero);
        HeroSelectionReady?.Invoke(dataLoader.SelectedHeros.Count == 3);
    }


    private void LoadGameplayScene()
    {
        SceneManager.LoadScene("Game");
    }

    private void OnApplicationQuit()
    {
        dataLoader.DataIsReady = false;
    }
}
