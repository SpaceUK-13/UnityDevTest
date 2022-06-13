using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class GameDataLoader : ScriptableObject
{
    public List<DataType> DataTypes;
    public List<HeroScriptableObject> DefaultHeroes;
    public Action LoadingDone;
  
    Action<IUserData, DataType> dataLoaded;
    Dictionary<DataType, IDataLoader> dataLoaders = new Dictionary<DataType, IDataLoader>();
    Dictionary<DataType, IUserData> gameData = new Dictionary<DataType, IUserData>();
    public bool DataIsReady {  get; set; }
    public List<HeroScriptableObject> AvailableHeros { private set; get; } = new List<HeroScriptableObject>();
    public List<HeroScriptableObject> SelectedHeros { private set; get; } = new List<HeroScriptableObject>();

    public void Init()
    {
        dataLoaded += DataLoadComplete;
        ClearOldData();
        LoadData();
    }
    private void LoadData()
    {
        if (DataIsReady)
            return;

        foreach (var key in DataTypes)
        {
            if (!dataLoaders.ContainsKey(key))
            {
                var loader = CreateDataLoader(key);
                loader.LoadData(dataLoaded);
                dataLoaders.Add(key, loader);
            }
        }
    }
     private IDataLoader CreateDataLoader(DataType type)
    {
        IDataLoader dataLoader = null;
        switch (type)
        {
            case DataType.PlayerCharacterData:
                dataLoader = new CharacterDataLoader(DefaultHeroes);
                break;
            case DataType.GameSceneData:
                break;
            default:
                break;
        }
        return dataLoader;
    }
    private void DataLoadComplete(IUserData data, DataType dataType)
    {
        if (!gameData.ContainsKey(dataType))
        {
            gameData.Add(dataType, data);
        }

        if (gameData.Count == DataTypes.Count)
        {
            DataIsReady = true;
            LoadingDone?.Invoke();
        }

    }
    public IUserData GetData(DataType dataType)
    {
        if (!gameData.ContainsKey(dataType))
        {
            Debug.LogWarning($"Can not fetch data of type {dataType}");
            return null;
        }
        return gameData[dataType];
    }
    public void AddAvilabeHeros(List<HeroScriptableObject> heros)
    {
        AvailableHeros = heros.ToList();
    }
    public void ToogleSelectedHeroes(bool addHero, HeroScriptableObject hero)
    {
        if (addHero)
            SelectedHeros.Add(hero);
        else
            SelectedHeros.Remove(hero);
    }
    private void ClearOldData()
    {
        SelectedHeros.Clear();
        AvailableHeros.Clear();
        gameData.Clear();
        dataLoaders.Clear();
    }
}
