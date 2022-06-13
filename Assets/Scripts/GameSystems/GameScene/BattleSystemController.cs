using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystemController : GameSceneController, IInteractionController
{
    [SerializeField] private GameCharacterController enemy;
    [SerializeField] private List<GameCharacterController> heroes;
    [SerializeField] private GameplayState gameplayState;

    GameCharacterController selectedHero;
    Action<GameCharacterController> characterAttackCallBack;
    Action<GameCharacterController> changeTurnCallBack;
    Action<Action<GameCharacterController>> characterTakeDamageCallBack;

    GameManager gameManager;

    public override void LoadDependencies(GameManager manager)
    {
        gameManager = manager;
        characterAttackCallBack += CharacterAttackDone;
        changeTurnCallBack += MoveToNextTurn;
        CreateHeroes();
    }

    private void CreateHeroes()
    {
        for (int i = 0; i < heroes.Count; i++)
        {
            var heroData = gameManager.GameData.SelectedHeros[i];
            heroes[i].SetUpHero(null);
        }   
    }

    public void SetObjectOfInterest(GameObject objectOfInterest)
    {
 
        if (objectOfInterest == null || (selectedHero = objectOfInterest.GetComponent<GameCharacterController>()) == null)
            return;

        if (!CheckIfPlayersTurn())
            return;
        
        if (selectedHero.CharacterType == CharacterType.Hero && selectedHero.IsCharacterEnabled)
            InitiateAttack();
        else
            selectedHero = null;
    
    }

    private bool CheckIfPlayersTurn()
    {
        return gameplayState == GameplayState.PlayerTurn;
    }

    private void InitiateAttack()
    {

        if (CheckIfPlayersTurn())
        {
            selectedHero.CharacterAttack(characterAttackCallBack);
            characterTakeDamageCallBack += enemy.CharacterTakeDamage(selectedHero.CharacterData.AttackPower);
            gameplayState = GameplayState.EnemyTurn;
        }
        else
        {
            if (CheckAvailableHeros())
            {
                enemy.CharacterAttack(characterAttackCallBack);
                characterTakeDamageCallBack += GetRandomHero().CharacterTakeDamage(enemy.CharacterData.AttackPower);
            }
            else
            {
                Debug.Log($"{enemy.CharacterData.Name} has won");
                gameManager.EndGameSession?.Invoke(GameplayState.GameLost);

            }
        }
    }


    private void CharacterAttackDone(GameCharacterController characterController)
    {
        Debug.Log($"{characterController.CharacterData.Name} has finished attacking ");
        characterTakeDamageCallBack?.Invoke(changeTurnCallBack);
        characterTakeDamageCallBack = null;
    }

    private void MoveToNextTurn(GameCharacterController characterController)
    {
        if (characterController.CharacterType == CharacterType.Hero)
        {
            gameplayState = GameplayState.PlayerTurn;
            if (!characterController.IsCharacterEnabled)
            {
                RemoveHeroFromQueue(characterController);
                if (!CheckAvailableHeros())
                {
                    Debug.Log($"{enemy.CharacterData.Name} has won");
                    gameManager.EndGameSession?.Invoke(GameplayState.GameLost);
                }
            }
        }
        else
        {
            if (characterController.IsCharacterEnabled)
            {
                InitiateAttack();
            }
            else
            {
                Debug.Log($"{characterController.CharacterData.Name} is dead");
                gameManager.EndGameSession?.Invoke(GameplayState.GameWon);
            }     
        }
    }

    private GameCharacterController GetRandomHero()
    {
        return heroes[UnityEngine.Random.Range(0,heroes.Count)];
    }

    private bool CheckAvailableHeros()
    {
        return heroes.Count > 0;
    }

    private void RemoveHeroFromQueue(GameCharacterController characterController)
    {
        heroes.Remove(characterController);
    }

    private void OnDestroy()
    {
        characterAttackCallBack -= CharacterAttackDone;
        changeTurnCallBack -= MoveToNextTurn;
    }
}
