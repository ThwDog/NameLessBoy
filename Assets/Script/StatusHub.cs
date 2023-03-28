using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusHub : MonoBehaviour
{
    public Image statusHPBar;
    public TMP_Text statusHPValue;
    CharecterStatus status; 

    void Update()
    {
        
    }

    public void SetStatusHUD(StatusData status)
    {
        float currentHealth = status.health * (100 / status.maxHealth);
       
        statusHPBar.fillAmount = currentHealth / 100;
        statusHPValue.SetText(status.health + "/" + status.maxHealth);
    }
    public void SetHP(StatusData status, float hp)
    {
        StartCoroutine(GraduallySetStatusBar(status, hp, false, 5, 0.05f));
    }
    public void SetHPHeal(StatusData status, float hp)
    {
        StartCoroutine(GraduallySetStatusBar(status, hp, true, 5, 0.05f));
    }

    IEnumerator GraduallySetStatusBar(StatusData status, float amount, bool isIncrease, int fillTimes, float fillDelay)
    {
        float percentage = 1 / (float)fillTimes;

        if (isIncrease)
        {
            for (int fillStep = 0; fillStep < fillTimes; fillStep++)
            {
                float _fAmount = amount * percentage;
                float _dAmount = _fAmount / status.maxHealth;
                status.health += _fAmount;
                statusHPBar.fillAmount += _dAmount;
                if (status.health <= status.maxHealth)
                    statusHPValue.SetText(status.health + "/" + status.maxHealth);
                yield return new WaitForSeconds(fillDelay);
            }
        }
        else
        {
            for (int fillStep = 0; fillStep < fillTimes; fillStep++)
            {
                float _fAmount = amount * percentage;
                float _dAmount = _fAmount / status.maxHealth;
                status.health -= _fAmount;
                statusHPBar.fillAmount -= _dAmount;
                if (status.health >= 0)
                    statusHPValue.SetText(status.health + "/" + status.maxHealth);

                yield return new WaitForSeconds(fillDelay);
            }
        }
    }
}
