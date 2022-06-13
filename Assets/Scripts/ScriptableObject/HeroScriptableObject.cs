using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Heroes", menuName = "HeroesObject", order = 1)]
public class HeroScriptableObject : ScriptableObject
{
    public string Name;
    public float Health;
    public float AttackPower;
    public int Level;
    public float Experience;
    public bool IsUnlocked;
    public Color HeroesColor;
}

