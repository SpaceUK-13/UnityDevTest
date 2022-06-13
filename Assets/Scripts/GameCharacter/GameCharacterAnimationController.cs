using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameCharacterAnimationController : GameCharacterComponent
{

    [SerializeField] TextMeshPro statusDisplay;
    [SerializeField] MeshRenderer gunBarrelMeshRendered;
    [SerializeField] MeshRenderer characterBodyMeshRendered;
    [SerializeField] GameObject gunSmokeParticalEffect;
    [SerializeField] Color attackAnimationColor, takeDamageAnimationColor;

    GameCharacterController characterController;
    Color gunBarrelColor,characterMaterial;
   
    private void Start()
    {
        gunSmokeParticalEffect.SetActive(false);
        gunBarrelColor = gunBarrelMeshRendered.material.color;
        characterMaterial = characterBodyMeshRendered.material.color;
    }

    public override void LoadCharacterDataToComponents(GameCharacterController controller)
    {
        characterController = controller;
    }

    public void SetOriginalColor(Color originalColor)
    {
        characterMaterial = originalColor;
    }

    public void PlayAttackAnumation(Action<GameCharacterController> attackDoneCallBack,float damageAmout)
    {

        Sequence attackAnimationSequence = DOTween.Sequence();
        attackAnimationSequence.Append(gunBarrelMeshRendered.material.DOColor(attackAnimationColor,1f).OnComplete(()=> 
        {
            gunSmokeParticalEffect.SetActive(true);
            statusDisplay.text = $"{damageAmout} damage done";
            attackDoneCallBack?.Invoke(characterController);
        }));
        attackAnimationSequence.Append(gunBarrelMeshRendered.material.DOColor(gunBarrelColor, 1f).OnComplete(() =>
        {
            gunSmokeParticalEffect.SetActive(false);
            statusDisplay.text = string.Empty;
        }));
        attackAnimationSequence.Play();
    }

    public void PlayDamageAnimation(Action<GameCharacterController> endOfAnimationCallBack)
    {
 
        Sequence takeDamageAnimationSequence = DOTween.Sequence();
        takeDamageAnimationSequence.Append(characterBodyMeshRendered.material.DOColor(takeDamageAnimationColor,0.5f));
        takeDamageAnimationSequence.Append(characterBodyMeshRendered.material.DOColor(characterMaterial,0.5f));
        takeDamageAnimationSequence.Append(characterBodyMeshRendered.material.DOColor(takeDamageAnimationColor, 0.5f));
        takeDamageAnimationSequence.Append(characterBodyMeshRendered.material.DOColor(characterMaterial,0.5f).OnComplete(()=> 
        {
            statusDisplay.text = characterController.IsCharacterEnabled ? string.Empty : "I AM DEAD";
            characterController.HealthUpdate(characterController.CurrentHealth);
            endOfAnimationCallBack.Invoke(characterController);
        }));
        takeDamageAnimationSequence.Play();
    }
}
