using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroSelectionIcon : MonoBehaviour
{
    [SerializeField] Image heroIcon;
    [SerializeField] Image border;
    [SerializeField] TextMeshProUGUI heroNameText;
    [SerializeField] Button iconButton;
    [SerializeField] GameObject lockImage;
    [SerializeField] Color borderSelectionColor;
    bool isSelected = false;
    MainMenuUIManager mainMenuUIManager;
    HeroScriptableObject hero;
   

    public string ID { private set; get; }
    

    public void AddHeroData(HeroScriptableObject h, MainMenuSceneManager sceneManager, MainMenuUIManager menuUI)
    {
        hero = h;
        mainMenuUIManager = menuUI;
        heroIcon.color = h.HeroesColor;
        heroNameText.text = h.Name;
        ID = h.Name;
        heroNameText.color = h.HeroesColor;
        iconButton.interactable = h.IsUnlocked;
        lockImage.SetActive(!h.IsUnlocked);
        iconButton.onClick.AddListener(() =>
        {
            SelectHero(sceneManager, h);
        });
        border.color = h.isSelected ? borderSelectionColor : Color.white;
    }


    private void SelectHero(MainMenuSceneManager sceneManage, HeroScriptableObject hero)
    {
        isSelected = !isSelected;
        border.color = isSelected ? borderSelectionColor : Color.white;
        sceneManage.HeroSelected?.Invoke(this, hero, isSelected);
    }

    public void ForceDeselect()
    {
        isSelected = false;
        border.color = Color.white;
    }

    public void UnlockHero(bool unlocked)
    {
        lockImage.SetActive(!unlocked);
        iconButton.interactable = unlocked;
    }

    public void OnHover()
    {
        mainMenuUIManager.ActivateStatsPanel(true,hero);
    }

    public void OnHoverExit()
    {
        mainMenuUIManager.ActivateStatsPanel(false,null);
    }

}
