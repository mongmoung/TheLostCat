using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Slot : MonoBehaviour
{
    public Inventory inventory;
    public Image ItemImage;
    public Item item;

    public void SlotItemUse()
    {
        if(item != null) 
        { 

            if(item.gameObject.tag == "Key" || item.gameObject.tag == "Equipment") 
            {
                item.Use(inventory.owner);
            }
            else
            {
                item.Use(inventory.owner);
                SetImage(null);

            }
        }
    }
    public void SetImage(Item setItem)
    {
        item = setItem;
        if (item == null)
            ItemImage.sprite = null;
        else
            ItemImage.sprite = item.sprite;
    }


}
