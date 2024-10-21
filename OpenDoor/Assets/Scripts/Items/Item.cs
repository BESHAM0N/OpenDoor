using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemType Type => _itemType;

    [SerializeField] private ItemType _itemType;   

    public void PickUp(List<Item> inventory)
    {
        if (!inventory.Contains(this))
        {
            inventory.Add(this);
            gameObject.SetActive(false);
            Debug.Log($"Подобран предмет: {_itemType}");
        }
    }

    public void Drop(List<Item> inventory, List<Item> chest)
    {
        var item = chest.FirstOrDefault(x => x.Type == Type);
       
        if (item == null) return;

        if (inventory.Contains(this))
        {
            item.gameObject.SetActive(true);
            inventory.Remove(this);
            Debug.Log($"Выложен предмет: {_itemType}");
        }
    }
}
