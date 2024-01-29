using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Creature, ICollidable
{
    [SerializeField]
    private float damageValue = 25;

    public void CollideAction(GameObject collider)
    {
        Player player = collider.GetComponent<Player>();
        if (player != null)
        {
            player.Damage(damageValue);
            player.GivePoints(1);
        }
        Destroy(this.gameObject);
    }
}
