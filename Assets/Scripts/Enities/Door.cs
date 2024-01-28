using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Thing, IInteractable
{
    private string DoorOpenAnimatorParameter = "DoorOpenClose";
    private Animator animator;
    private bool isOpen = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void interactionAction(GameObject interactionSource)
    {
        if (isOpen)
        {
            isOpen = false;
        }
        else 
        {
            isOpen = true;
        }
        animator.SetBool(DoorOpenAnimatorParameter, isOpen);
    }
}
