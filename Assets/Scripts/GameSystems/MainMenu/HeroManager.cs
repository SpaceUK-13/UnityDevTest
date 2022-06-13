using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour
{

    GameDataLoader gameData;
    MainMenuSceneManager sceneManager;
    List<HeroScriptableObject> availableHero = new List<HeroScriptableObject>();
    public void Init(GameDataLoader data, MainMenuSceneManager manager)
    {
        gameData = data;
        sceneManager = manager;
        CreateHeros();
    }

    private void CreateHeros()
    {
        var heroes = gameData.GetData(DataType.PlayerCharacterData) as CharacterPersitanatData;
        foreach (var storedHero in heroes.PlayerCharacters)
        {
            Color color = ColorUtility.TryParseHtmlString(storedHero.ColorCode, out color) ? color : Color.white;
            HeroScriptableObject hero = ScriptableObject.CreateInstance<HeroScriptableObject>();

            hero.AttackPower = storedHero.AttackPower;
            hero.Name = storedHero.Name;
            hero.Level = storedHero.Level;
            hero.Experience = storedHero.Experience;
            hero.Health = storedHero.Health;
            hero.IsUnlocked = storedHero.IsUnlocked;
            hero.HeroesColor = color;

            availableHero.Add(hero);
        }
        gameData.AddAvilabeHeros(availableHero);
        sceneManager.HeroesReady?.Invoke();
    }
}
