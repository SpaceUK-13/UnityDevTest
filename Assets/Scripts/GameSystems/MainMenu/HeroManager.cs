using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroManager : MonoBehaviour
{

    GameDataController gameData;
    MainMenuSceneManager sceneManager;
    private const float numberOfGamesCheck =5f; 
    public void Init(GameDataController data, MainMenuSceneManager manager)
    {
        gameData = data;
        sceneManager = manager;
        CreateHeros();
        UpgradeHero();
    }

    private void CreateHeros()
    {
        var heroes = gameData.GetData(DataType.PlayerCharacterData) as CharacterPersitanatData;
        var availableHero = new List<HeroScriptableObject>();
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

    private void UpgradeHero()
    {
        if (gameData.SelectedHeros.Count <= 0)
            return;

        var playerProgress = gameData.GetData(DataType.PlayerProgressionData) as PlayerProgressionData;
        UnlockNewHero(playerProgress);
        if (playerProgress.PlayersConsecutiveWins > 0)
            AddExpirinceToHeroes();
        gameData.SelectedHeros.Clear();
        SaveHeroes();
        sceneManager.HeroesReady?.Invoke();
    }

    private void AddExpirinceToHeroes()
    {
        var upgradedHeroes = new List<HeroScriptableObject>();
        foreach (var hero in gameData.SelectedHeros)
        {
            if (hero.HeroSurvived)
            {
                hero.Experience += 1;
                upgradedHeroes.Add(LevelUpHero(hero));
            }
        }

        foreach (var hero in upgradedHeroes)
        {
             var tempHero = gameData.AvailableHeros.Find(x => x.Name == hero.Name);
             var index = gameData.AvailableHeros.IndexOf(tempHero);
             gameData.AvailableHeros[index] = hero;
        }
    }

    private HeroScriptableObject LevelUpHero(HeroScriptableObject hero)
    {
        if(hero.Experience>=numberOfGamesCheck)
        {
            hero.Level += 1;
            hero.Health += (hero.Health / 10f);
            hero.AttackPower += (hero.AttackPower / 10f);
            hero.Experience = 0;
        }

        return hero;
    }

    private void UnlockNewHero(PlayerProgressionData progressionData)
    {
        if(progressionData.PlayersNumberOfMathces%numberOfGamesCheck==0)
        {
            gameData.AvailableHeros.Find(x => x.IsUnlocked == false).IsUnlocked = true;
        }
    }


    private void SaveHeroes()
    {
        var newHeroData = (CharacterPersitanatData)gameData.GetData(DataType.PlayerCharacterData);
        newHeroData.PlayerCharacters.Clear();
        foreach (var item in gameData.AvailableHeros)
        {
            newHeroData.PlayerCharacters.Add(ConvertHeroToData(item));
        }
        gameData.SaveData(newHeroData,DataType.PlayerCharacterData);

    }

    private Hero ConvertHeroToData(HeroScriptableObject heroScriptable)
    {
        var hero = new Hero
        {
            Name = heroScriptable.Name,
            AttackPower = heroScriptable.AttackPower,
            Health = heroScriptable.Health,
            Experience = heroScriptable.Experience,
            Level = heroScriptable.Level,
            IsUnlocked = heroScriptable.IsUnlocked,
            ColorCode = "#"+ColorUtility.ToHtmlStringRGB(heroScriptable.HeroesColor)
        };
        return hero;
    }

}
