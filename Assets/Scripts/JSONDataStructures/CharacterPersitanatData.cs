
using System.Collections.Generic;

[System.Serializable]
public class CharacterPersitanatData : IUserData
{
    public DataType DataType { get; set; }
    public List<Hero> PlayerCharacters = new List<Hero>();

   
}

 
