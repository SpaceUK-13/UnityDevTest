using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerProgressionData : IUserData
{
    public DataType DataType { get; set; }
    public int PlayersNumberOfMathces;
    public int PlayersConsecutiveWins;

}
