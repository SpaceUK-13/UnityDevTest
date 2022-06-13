using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSelectionPanel : MonoBehaviour
{
    [SerializeField] GameObject heroSelectionIcon;
    [SerializeField] Transform parentPanel;
    public void CreateHeroIcon(HeroScriptableObject hero,MainMenuSceneManager sceneManager)
    {
        var heroIcon = Instantiate(heroSelectionIcon);
        heroIcon.transform.SetParent(parentPanel);
        heroIcon.transform.localScale = new Vector3(1f,1f,1f);
        heroIcon.transform.localPosition = Vector3.zero;
        var icon = heroIcon.GetComponent<HeroSelectionIcon>();
        icon.AddHeroData(hero,sceneManager);

    }
}
