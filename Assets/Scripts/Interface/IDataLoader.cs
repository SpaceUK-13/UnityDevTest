using System;


public interface IDataLoader
{
    void LoadData(Action<IUserData,DataType> dataLoaded);

    void SaveData(IUserData data);
}
