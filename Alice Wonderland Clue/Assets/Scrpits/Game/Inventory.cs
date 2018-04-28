using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : Singleton<Inventory>
{
    public List<InventoryItem> items = new List<InventoryItem>();
    public Image[] images;
    public Animator[] animators;

    public void Add(string potionName, Sprite potionUI, bool displayUI = false)
    {
        if (items.Count >= images.Length) return;   // stack is full

        int index = items.Count;
        InventoryItem item = new InventoryItem
        {
            name = potionName,
            image = images[index],
            animator = animators[index],
        };
        item.animator.SetInteger("slot", index + 1);
        item.image.sprite = potionUI;
        if (displayUI) item.DisplayItemAnim();
        items.Add(item);
    }

    public InventoryItem Use(string potionName, bool displayUI = false)
    {
        InventoryItem foundOne = null;
        foreach (InventoryItem item in items)
        {
            if (potionName.Equals(item.name))
            {
                // Found it 
                if (displayUI) item.HideItemAnim();

                foundOne = item;
                break;
            }
        }
        if(foundOne!=null)
        {
            items.Remove(foundOne);
            List<InventoryItem> oldList = items;
            items = new List<InventoryItem>();
            foreach (InventoryItem item in oldList)
            {
                Add(item.name, item.image.sprite);
            }
        }
        return foundOne;
    }
}

public class InventoryItem
{
    public string name;
    public Image image;
    public Animator animator;

    public void DisplayItemAnim()
    {
        animator.gameObject.SetActive(true);
    }

    public void HideItemAnim()
    {
        animator.gameObject.SetActive(false);
    }
}
