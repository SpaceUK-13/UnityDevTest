using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsPanelDisplay : MonoBehaviour
{
    [SerializeField] Transform statsPanel;
    [SerializeField][Range(1f,10f)]  float timer=3f;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI attackText;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI expirinceText;

    bool timerActivated,panelDisplayed;
    float timeLeft;

    HeroScriptableObject hero;

    private void Start()
    {
        statsPanel.gameObject.SetActive(false);
        panelDisplayed = false;
        timerActivated = false;
        timeLeft = timer;
    }

    private void Update()
    {
        if (timerActivated)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0 && !panelDisplayed)
            {
                DisplayPanel();
            }
        }
    }

    private void DisplayPanel()
    {
        panelDisplayed = true;
        statsPanel.gameObject.SetActive(true);

        if(hero!=null)
        {
            nameText.text= hero.Name;
            attackText.text = $"Attack : {hero.AttackPower.ToString()}";
            healthText.text =  $"Health : {hero.Health.ToString()}";
            levelText.text =  $"Level : {hero.Level.ToString()}";
            expirinceText.text =  $"Expirince : {hero.Experience.ToString()}";
        }
    }

    public void StartPanelDisplayTimer(HeroScriptableObject hero)
    {
        if (timerActivated)
            return;

        timerActivated = true;
        timeLeft = timer;
    }

    public void EndPanelDisplayTimer()
    {
        if(statsPanel.gameObject.activeInHierarchy)
          statsPanel.gameObject.SetActive(false);

        hero = null;
        timeLeft = timer;
        timerActivated = false;
        panelDisplayed = false;
    }
}
