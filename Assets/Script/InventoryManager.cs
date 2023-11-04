using System.Buffers.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
   public static InventoryManager Instance;
   public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    public Toggle EnableRemove;
    private InventoryItemController[] InventoryItems;
    private bool isInventoryOpen = false;


    private void Awake()
    {
        Instance = this;
    }
    public void Add(Item item)
    {
        Items.Add(item);
    }
    public void Remove(Item item)
    {
        Items.Remove(item);
    }


    public void ToggleInventory()
    {
        if (isInventoryOpen)
        {
            CloseInventory();
            isInventoryOpen = false;
        }
        else
        {
            OpenInventory();
            isInventoryOpen = true;
        }
    }

    private void OpenInventory()
    {
        ListItem();
        // Logic to open the inventory
        Debug.Log("Opening inventory...");
    }

    private void CloseInventory()
    {
        DestroyItem();
        // Logic to close the inventory
        Debug.Log("Closing inventory...");
    }

    public void ListItem()
    {

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();
            
            itemIcon.sprite = item.icon;
            itemName.text = item.itemName;

            if (EnableRemove.isOn)
            {
                removeButton.gameObject.SetActive(true);
            }
        }

        SetInventoryItems();

    }
    public void DestroyItem()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
    }
    public void EnableItemRemove()
    {
        if (EnableRemove.isOn)
        {
            foreach (Transform Item in ItemContent)
            {
                Item.Find("RemoveButton").gameObject.SetActive(true);
            }
        }else
        {
            foreach (Transform Item in ItemContent)
            {
                Item.Find("RemoveButton").gameObject.SetActive(false);
            }
        }
    }

    public void SetInventoryItems()
    {
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < Items.Count; i++)
        {
            if (i < InventoryItems.Length)
            {
                InventoryItems[i].AddItem(Items[i]);
            }
            else
            {
                Debug.LogError("Index out of range: InventoryItems array is smaller than the number of items.");
            }
        }
    }
 
}
