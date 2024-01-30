using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Creature, ICollidable
{
    [SerializeField]
    private float damageValue = 25;
    [SerializeField]
    private int enemiesAmount = 1;

    public void CollideAction(GameObject collider)
    {
        Player player = collider.GetComponent<Player>();
        if (player != null)
        {
            CombatManager.instance.StartBattle(enemiesAmount);
            player.GivePoints(1);
        }
        Destroy(this.gameObject);
    }
}
