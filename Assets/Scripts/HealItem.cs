using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : Entity
{
    [SerializeField]
    private float healValue = 30;

    public override void collideAction(GameObject collider)
    {
        collider.GetComponent<Player>()?.Damage(-healValue);
        Destroy(this.gameObject);
    }
}
