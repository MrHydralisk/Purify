using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICombatEnemy : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public CombatCreature combatCreature;
    public Slider sliderHP;
    public Image image;
    public GameObject selectedIcon;
    public bool isSelected
    {
        get
        {
            return selectedIcon.activeSelf;
        }
        set
        {
            selectedIcon.SetActive(value);
            ShowHealth(value);
        }
    }

    private void Start()
    {
        image = GetComponent<Image>();
        image.alphaHitTestMinimumThreshold = 1f; //prevent raycast on transparent
    }

    public void SetHealth(float value) 
    {  
        sliderHP.value = value;
    }

    public void ShowHealth(bool value = true)
    {
        sliderHP.gameObject.SetActive(value);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowHealth(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isSelected)
        {
            ShowHealth(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CombatManager.instance.ChangeSelectedEnemy(combatCreature);
    }
}
        