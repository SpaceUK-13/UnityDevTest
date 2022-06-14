using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgressionDataLoader : IDataLoader
{
    private const string fileName = "PlayerProgression";
    private PlayerProgressionData localProgressionData;
    public void LoadData(Action<IUserData, DataType> dataLoaded)
    {
        if (FileUtilities.CheckIfFileExits(fileName))
        {
            try
            {
                localProgressionData = FileUtilities.LoadProgresionData(fileName);
                if (localProgressionData == null)
                {
                    localProgressionData = new PlayerProgressionData();
                    localProgressionData.DataType = DataType.PlayerProgressionData;
                    Debug.LogError("Error loading locally stored Data");
                }
                else
                {
                    localProgressionData.DataType = DataType.PlayerProgressionData;
                }
                dataLoaded?.Invoke(localProgressionData, DataType.PlayerProgressionData);
            }
            catch (Exception e)
            {
                localProgressionData = new PlayerProgressionData();
                Debug.LogError($"Error loading locally stored Data {e.Message}");
            }
        }
        else
        {
            Debug.Log($"Creating file of type : {fileName}");
            localProgressionData = new PlayerProgressionData();
            FileUtilities.SaveFileToJson(JsonUtility.ToJson(localProgressionData), fileName);
            dataLoaded?.Invoke(localProgressionData, DataType.PlayerProgressionData);
        }
    }

    public void SaveData(IUserData data)
    {
        var newData = (PlayerProgressionData)data;
        FileUtilities.SaveFileToJson(JsonUtility.ToJson(newData), fileName);
    }
}
