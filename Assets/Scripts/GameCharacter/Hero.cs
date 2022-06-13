using UnityEngine;

[System.Serializable]
public class Hero : ICharacter
{
    public string Name;
    public float Health;
    public float AttackPower;
    public int Level;
    public float Experience;
    public bool IsUnlocked;
    public string ColorCode;

}
