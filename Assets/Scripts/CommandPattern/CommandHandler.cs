using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandHandler : ICommand
{
    public void ScreenInteraction(ShootRaycast shootRaycast, IInteractionController interactionController)
    {
        interactionController.SetObjectOfInterest(shootRaycast.RaycastHit());
    }
}
