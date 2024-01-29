using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Thing, IInteractable
{
    [SerializeField]
    private int energyValue = 5;

    public void InteractionAction(GameObject interactionSource)
    {
        interactionSource.GetComponent<Player>()?.GivePoints(energyValue);
        Destroy(this.gameObject);
    }
}
