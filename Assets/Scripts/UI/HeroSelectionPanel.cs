using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSelectionPanel : MonoBehaviour
{
    [SerializeField] GameObject heroSelectionIcon;
    [SerializeField] Transform parentPanel;
    List<HeroSelectionIcon> icons = new List<HeroSelectionIcon>();
    public void CreateHeroIcon(HeroScriptableObject hero,MainMenuSceneManager sceneManager,MainMenuUIManager menuUI)
    {
        if (CheckIfHeroIconExitsts(hero))
            return;

        var heroIcon = Instantiate(heroSelectionIcon);
        heroIcon.transform.SetParent(parentPanel);
        heroIcon.transform.localScale = new Vector3(1f,1f,1f);
        heroIcon.transform.localPosition = Vector3.zero;
        var icon = heroIcon.GetComponent<HeroSelectionIcon>();
        icons.Add(icon);
        icon.AddHeroData(hero,sceneManager,menuUI);
    }


    private bool CheckIfHeroIconExitsts(HeroScriptableObject hero)
    {
        var tempIcon = icons.Find(x => x.ID == hero.Name);
        if(tempIcon!=null)
        {
            tempIcon.UnlockHero(hero.IsUnlocked);
            return true;
        }
        return false;
    }
}
