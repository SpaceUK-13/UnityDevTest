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
    [SerializeField] Color borderSelectionColor;
    [SerializeField] Button iconButton;
    [SerializeField] GameObject lockImage;
    private bool isSelected = false;

    public void AddHeroData(HeroScriptableObject hero, MainMenuSceneManager sceneManager)
    {
        heroIcon.color = hero.HeroesColor;
        heroNameText.text = hero.Name;
        heroNameText.color = hero.HeroesColor;
        iconButton.interactable = hero.IsUnlocked;
        lockImage.SetActive(!hero.IsUnlocked);
        iconButton.onClick.AddListener(() =>
        {
            SelectHero(sceneManager,hero);
        });
    }


    private void SelectHero(MainMenuSceneManager sceneManage, HeroScriptableObject hero)
    {
        isSelected = !isSelected;
        border.color = isSelected ? borderSelectionColor : Color.white;
        sceneManage.HeroSelected?.Invoke(this, hero,isSelected);

    }

    public void ForceDeselect()
    {
        isSelected = false;
        border.color = Color.white;
    }
}
