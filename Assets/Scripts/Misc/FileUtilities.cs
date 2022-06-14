using System.IO;
using UnityEngine;

public class FileUtilities
{
    private static string appDataPath
    {
        get { return Application.persistentDataPath; }
    }

    public static bool CheckIfFileExits(string fileName)
    {
        var filePath = CreateFilePath(fileName);
        return File.Exists(filePath);   
    }

    public static void SaveFileToJson(string file,string fileName)
    {
        File.WriteAllText(CreateFilePath(fileName), file);
    }

    public static CharacterPersitanatData LoadPlayerCharacterData(string fileName)
    {
        return JsonUtility.FromJson<CharacterPersitanatData>(File.ReadAllText(CreateFilePath(fileName)));
    }

    public static PlayerProgressionData LoadProgresionData(string fileName)
    {
        return JsonUtility.FromJson<PlayerProgressionData>(File.ReadAllText(CreateFilePath(fileName)));
    }

    private static string CreateFilePath(string fileName)
    {
        return Path.Combine(appDataPath, fileName)+".json";
    }

}
