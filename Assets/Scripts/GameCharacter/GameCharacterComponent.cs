using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameCharacterComponent : MonoBehaviour
{
    public abstract void LoadCharacterDataToComponents(GameCharacterController controller);
}
