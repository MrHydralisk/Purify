using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Thing, IInteractable
{
    private string DoorOpenAnimatorParameter = "DoorOpenClose";
    private Animator animator;
    private bool isOpen = false;

    [Header("Button locked")]
    [SerializeField]
    private bool isLocked = false;
    [SerializeField]
    private Button relatedPanel;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void InteractionAction(GameObject interactionSource)
    {
        if (isLocked)
        {
            if (relatedPanel != null)
            {
                StartCoroutine(relatedPanel.LineDraw());
            }
        }
        else
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

    public void LockStateChange()
    {
        isLocked = !isLocked;
    }
}
