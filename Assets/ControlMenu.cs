using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMenu : MonoBehaviour

{
    public GameObject ControlUI;
    
    public void ShowMenu()
    {
        bool isActive = ControlUI.activeSelf;
        ControlUI.SetActive(!isActive);
    }
}
