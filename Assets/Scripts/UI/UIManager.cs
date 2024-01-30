using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }
    [SerializeField]
    public GameObject uIObject;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void DisableUI()
    {
        uIObject.SetActive(false);
    }

    public void EnableUI()
    {
        uIObject.SetActive(true);
    }
}
