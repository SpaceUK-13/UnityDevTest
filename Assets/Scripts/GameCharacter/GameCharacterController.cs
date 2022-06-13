using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCharacterController : MonoBehaviour
{

    public Action<float> HealthUpdate;
    public bool IsCharacterEnabled { private set; get; } = true;
    public float CurrentHealth { private set; get; }
    public CharacterHealthState CharacterCurrentLiveState => characterState;
    public CharacterType CharacterType => characterType;

    [Header("Character Components")]
    public HeroScriptableObject CharacterData;
    [SerializeField] GameCharacterAnimationController gameCharacterAnimationController;
    [SerializeField] GameCharacterHealthController gameCharacterHealthController;
    [SerializeField] GameCharacterUIController gameCharacteUIController;
    [SerializeField] MeshRenderer heroMeshRenderer; 
    [SerializeField] CharacterType characterType;


    CharacterHealthState characterState = CharacterHealthState.Healthy;

    private void Start()
    {
        gameCharacterHealthController.LoadCharacterDataToComponents(this);
        gameCharacteUIController.LoadCharacterDataToComponents(this);
        gameCharacterAnimationController.LoadCharacterDataToComponents(this);
    }

    public void SetUpHero(HeroScriptableObject hero)
    {
        CharacterData = hero==null? CharacterData : hero;
        heroMeshRenderer.material.color = CharacterData.HeroesColor;
        gameCharacterAnimationController.SetOriginalColor(CharacterData.HeroesColor);
    }

    public void CharacterAttack(Action<GameCharacterController> attackCallBack)
    {
        gameCharacterAnimationController.PlayAttackAnumation(attackCallBack, CharacterData.AttackPower);
    }

    public Action<Action<GameCharacterController>> CharacterTakeDamage(float damageAmount)
    {
        ApplyDamageToPlayer(damageAmount);
        return gameCharacterAnimationController.PlayDamageAnimation;
    }

    private void ApplyDamageToPlayer(float damageAmaount)
    {
        characterState = gameCharacterHealthController.UpdateHealth(damageAmaount);
        IsCharacterEnabled = characterState != CharacterHealthState.Dead;
        CurrentHealth = gameCharacterHealthController.GetCurrentHealth();
        Debug.Log($"{CharacterData.Name} current state is {characterState}");
    }
}
