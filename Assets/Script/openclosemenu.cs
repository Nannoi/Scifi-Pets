using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class openclosemenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject menu;
    void Start()
    {

    }


    // Update is called once per frame
    public void ClickMenu()
    {
        if (menu != null)
        {
            bool isActive = menu.activeSelf;
            menu.SetActive(!isActive); // Toggle the menu's active state
        }
    }
}