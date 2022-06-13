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

            Debug.LogWarning($"Added data of type : {dataType}");
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
            Debug.LogError($"Can not find data of type :{dataType}");
            return null;
        }
      
        var data = gameData[dataType];

        if (data == null)
            Debug.LogError($"Data of type :{dataType} is null");
           
        return data;
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
    public void ClearOldData()
    {
        DataIsReady = false;
        SelectedHeros.Clear();
        AvailableHeros.Clear();
        gameData.Clear();
        dataLoaders.Clear();
    }
}
