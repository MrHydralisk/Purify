using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public override void collideAction(GameObject collider)
    {
        Destroy(this.gameObject);
    }
}
