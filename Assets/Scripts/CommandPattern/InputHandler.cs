using UnityEngine;

public class InputHandler : GameSceneController, ICameraComponent
{
    BattleSystemController battleSystem;
    CommandHandler commandHandler = new CommandHandler();
    GameManager gameManager;
    bool enableInput =true;

    public override void LoadDependencies(GameManager manager)
    {
        gameManager = manager;
        battleSystem =  (BattleSystemController)gameManager.GetGameControllerComponent(typeof(BattleSystemController));
        if (battleSystem == null)
        {
            enableInput = false;
            Debug.LogError($"{typeof(BattleSystemController)} is not found!!!");
            return;
        }
    }
    public Camera GetSceneCamera()
    {
        return  gameManager.GetSceneCamera();
    }

    void Update()
    {
        if(!enableInput)
            return;

        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase.Equals(TouchPhase.Began))
            {
                ShootRaycast shootRaycast = new ShootRaycast(this, Input.GetTouch(0).position);
                commandHandler.ScreenInteraction(shootRaycast,battleSystem);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            ShootRaycast shootRaycast = new ShootRaycast(this, Input.mousePosition);
            commandHandler.ScreenInteraction(shootRaycast,battleSystem);
        }

        
    }

}
