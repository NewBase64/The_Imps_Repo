//CREATED


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int shieldHp;
    public int shieldCurrentHp;
    [SerializeField] int regenTimer;
    [SerializeField] int delayTimer = 3;
    public bool isActive = true;
    public bool regenning = false;
    public bool canRegen = true;

    private void Start()
    {
        shieldCurrentHp = shieldHp;
    }

    private void Update()
    {
        if (shieldCurrentHp < shieldHp && !regenning && canRegen)
        {
            regenning = true;
            StartCoroutine(HpRegen());
        }
    }

    public void takeDamage(int dmg)
    {
        if (dmg > shieldCurrentHp)
        {
            isActive = false;
            gamemanager.instance.playerScript.takeDamage(dmg - shieldCurrentHp);
            shieldCurrentHp = 0;

            gamemanager.instance.playerScript.updateShieldHp();
            gamemanager.instance.ShieldIndicator.color = new Color(1, 1, 1, 0.68f);
        }
        else
        {
            shieldCurrentHp -= dmg;
            if (shieldCurrentHp <= 0)
            {
                isActive = false;
                shieldCurrentHp = 0;

                gamemanager.instance.ShieldIndicator.color = new Color(1, 1, 1, 0.68f);
            }

            gamemanager.instance.playerScript.updateShieldHp();
        }
        StopAllCoroutines();
        canRegen = false;
        regenning = false;
        StartCoroutine(delay());
        
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(delayTimer);
        canRegen = true;
    }

    IEnumerator HpRegen()
    {
        while (shieldCurrentHp < shieldHp)
        {
            if (shieldCurrentHp > 0)
            {
                isActive = true;

                gamemanager.instance.ShieldIndicator.color = new Color(1, 1, 1, 1);
            }
            else
            {
                isActive = false;

                gamemanager.instance.ShieldIndicator.color = new Color(1, 1, 1, 0.68f);
            }
            yield return new WaitForSeconds(regenTimer);
            shieldCurrentHp++;

            gamemanager.instance.playerScript.updateShieldHp();
        }
        regenning = false;
    }
}
