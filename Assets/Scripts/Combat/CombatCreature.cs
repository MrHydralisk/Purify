using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCreature
{
    public UICombatEnemy UIElements;
    public float HP = 100;

    public void Damage(float amount)
    {
        HP -= amount;
        if (HP <= 0)
        {
            CombatManager.instance.DestroyEnemy(this);
        }
    }
}
