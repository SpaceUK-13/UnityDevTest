using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreenPopUp : MonoBehaviour
{
  [SerializeField] Image splashScreen;
  public void ToggleSplashScreen(bool toogle)
  {
        if (!splashScreen.gameObject.activeInHierarchy)
            splashScreen.gameObject.SetActive(true);

        var aplhaValue = toogle ? 1f : 0f;
        splashScreen.DOFade(aplhaValue,1.5f).OnComplete(()=> 
        {
            splashScreen.gameObject.SetActive(toogle);
        });
        
  }
}
