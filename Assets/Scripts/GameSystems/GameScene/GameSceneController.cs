using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameSceneController : MonoBehaviour
{
    public abstract void LoadDependencies(GameManager gameManager);
 
}