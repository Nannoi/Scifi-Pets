using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
   public static PuzzleManager Instance;
   public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;
    public InventoryItemController[] PuzzleItems;

    private void Awake()
    {
        Instance = this;
    }
    public void Add(Item item)
    {
      Items.Add(item);
      ScifiDoor.Instance.IncreasePiece(item.value);
      ListItem();
    }

    public void ListItem()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach(var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            
            itemIcon.sprite = item.icon;
        }

        SetInventoryItems();
    }

    public void SetInventoryItems()
    {
        PuzzleItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < Items.Count; i++)
        {
            PuzzleItems[i].AddItem(Items[i]);
        }
    }
}
