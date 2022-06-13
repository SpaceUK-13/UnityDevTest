using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCharacterHealthController : GameCharacterComponent
{
    GameCharacterController characterController;
    CharacterHealthState currentHealthState;
    float currentHealth,startingHealth;

    public override void LoadCharacterDataToComponents(GameCharacterController controller)
    {
        characterController = controller;
        currentHealth = characterController.CharacterData.Health;
        startingHealth = characterController.CharacterData.Health;
        currentHealthState = characterController.CharacterCurrentLiveState;
    }


    public CharacterHealthState UpdateHealth(float adjustAmount)
    {
        currentHealth -= adjustAmount;
        return UpdatePlayersHealthState(currentHealth);
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

   private CharacterHealthState UpdatePlayersHealthState(float health)
   {
        if (health <= 0)
            return currentHealthState = CharacterHealthState.Dead;
        else if (health <= startingHealth / 2)
            return currentHealthState = CharacterHealthState.Injured;
        else
            return currentHealthState = CharacterHealthState.Healthy;
   }

}
