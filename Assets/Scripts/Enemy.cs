using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField]
    private float damageValue = 25;

    public override void collideAction(GameObject collider)
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
