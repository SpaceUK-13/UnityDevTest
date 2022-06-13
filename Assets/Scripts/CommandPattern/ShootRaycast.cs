using UnityEngine;
public class ShootRaycast
{
    private Camera rayCastCamera;
    private Vector3 screenInputPosition;
    public Vector2 RayCastPointMatrix { private set; get; }
    public ShootRaycast(ICameraComponent camera, Vector3 clickPosition)
    {
        rayCastCamera = camera.GetSceneCamera();
        screenInputPosition = clickPosition;
    }


    public GameObject RaycastHit()
    {
        RaycastHit hit;
        Ray ray = rayCastCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
            return hit.transform.gameObject;
        else
            return null;
    }
}


