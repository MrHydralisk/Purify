using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : Thing, ICollidable
{
    [SerializeField]
    private float healValue = 30;

    public void collideAction(GameObject collider)
    {
        collider.GetComponent<Player>()?.Damage(-healValue);
        Destroy(this.gameObject);
    }
}
