
public enum DataType
{ 
    PlayerCharacterData,
    PlayerProgressionData
}

public enum CharacterType
{ 
    Hero, 
    Enemy
}

public enum GameplayState
{ 
    PlayerTurn, 
    EnemyTurn,
    GameWon,
    GameLost
}

public enum CharacterHealthState
{ 
    Healthy,
    Injured,
    Dead
}
