using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataLoader : IDataLoader
{
    private List<HeroScriptableObject> defaultCharacters = new List<HeroScriptableObject>();
    private const string characterDataFileName = "PlayerCharacters";
    private CharacterPersitanatData localPersitanatData = new CharacterPersitanatData();


    public CharacterDataLoader(List<HeroScriptableObject> defaultHeroes)
    {
        defaultCharacters = defaultHeroes;
    }
    public void LoadData(Action<IUserData,DataType> dataLoaded)
    {
        if (FileUtilities.CheckIfFileExits(characterDataFileName))
        {
            try
            {
                localPersitanatData = FileUtilities.LoadPlayerCharacterData(characterDataFileName);
                if (localPersitanatData.PlayerCharacters.Count == 0)
                {
                    localPersitanatData = CreateNewCharacterData();
                    Debug.LogError("Error loading locally stored Data");
                }
                else
                {
                    localPersitanatData.DataType = DataType.PlayerCharacterData;
                    dataLoaded?.Invoke(localPersitanatData,DataType.PlayerCharacterData);
                }
            }
            catch (Exception e)
            {
                localPersitanatData = CreateNewCharacterData();
                Debug.LogError($"Error loading locally stored Data {e.Message}");
            }
        }
        else
        {
            localPersitanatData = CreateNewCharacterData();
            FileUtilities.SaveFileToJson(JsonUtility.ToJson(localPersitanatData), characterDataFileName);
            dataLoaded?.Invoke(localPersitanatData, DataType.PlayerCharacterData);
        }
    }

    private CharacterPersitanatData CreateNewCharacterData()
    {
        var data = new CharacterPersitanatData();
        foreach (var item in defaultCharacters)
        {
            data.PlayerCharacters.Add(CreateCreateHero(item));
        }
        data.DataType = DataType.PlayerCharacterData;
        return data;
    }

    private Hero CreateCreateHero(HeroScriptableObject heroScriptableObject)
    {

        var hero = new Hero
        {
            AttackPower = heroScriptableObject.AttackPower,
            Level = heroScriptableObject.Level,
            Health = heroScriptableObject.Health,
            Experience = heroScriptableObject.Experience,
            Name = heroScriptableObject.Name,
            IsUnlocked = heroScriptableObject.IsUnlocked,
            ColorCode = "#"+ColorUtility.ToHtmlStringRGB(heroScriptableObject.HeroesColor)
        };
        return hero;
    }

    public void SaveData(IUserData data)
    {
      var newData = (CharacterPersitanatData)data;
      FileUtilities.SaveFileToJson(JsonUtility.ToJson(newData), characterDataFileName);
    }
}
