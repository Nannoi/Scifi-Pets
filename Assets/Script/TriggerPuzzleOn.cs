using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TriggerPuzzleOn: MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Panel;

    void OpenWindow()
    {
        if (Panel != null)
        {
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive); // Toggle the menu's active state
        }
    }
    private void OnMouseDown()
    {
        OpenWindow();
    }
}