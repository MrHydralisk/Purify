using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICombatEnemy : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Slider sliderHP;
    public Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        image.alphaHitTestMinimumThreshold = 1f; //prevent raycast on transparent
    }

    public void SetHealth(float value) 
    {  
        sliderHP.value = value;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        sliderHP.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        sliderHP.gameObject.SetActive(false);
    }
}
        