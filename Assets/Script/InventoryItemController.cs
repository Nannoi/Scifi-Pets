using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    Item item;
    public Button RemoveButton;
    public SwitchCharacter switchCharacter;
    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);
        Destroy(gameObject);
        Debug.Log("Item remove: " + item.name);
    }
    public void AddItem(Item newItem)
    {
        item = newItem;
        Debug.Log("Item added: " + item.name);
    }

    public void UseItem()
    {
        GameObject switchCharacterObject = GameObject.Find("Character Switcher");
        SwitchCharacter switchCharacter = switchCharacterObject.GetComponent<SwitchCharacter>();
        if (item == null)
        {
            Debug.Log("Item is null. Cannot use the item.");
            return;
        }
        else
        {
            switch (item.itemtype)
            {
                case Item.ItemType.Potion:
                    HealthManager.Instance.IncreaseHealth(switchCharacter.currentCharacter.name, item.value); ;
                    break;
                case Item.ItemType.Puzzle:
                    PuzzleManager.Instance.Add(item);
                    break;

            }
            RemoveItem();
        }
    }
}
