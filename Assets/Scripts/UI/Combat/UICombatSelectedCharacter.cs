using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class UICombatSelectedCharacter : MonoBehaviour
{
    private string OpenAnimatorParameter = "OpenClose";
    private Animator cachedAnimator;
    private Animator animator
    {
        get
        {
            return cachedAnimator ?? (cachedAnimator = GetComponent<Animator>());
        }
    }
    [SerializeField]
    private Image SelectedCharacterImage;

    public void OpenSidePanel()
    {
        animator.SetBool(OpenAnimatorParameter, true);
    }

    public void CloseSidePanel()
    {
        animator.SetBool(OpenAnimatorParameter, false);
    }

    public void UpdateSelectedCharacter(Sprite sprite)
    {
        SelectedCharacterImage.sprite = sprite;
    }
}
