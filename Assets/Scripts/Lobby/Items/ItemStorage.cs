using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemStorage", menuName = "ItemStorage")]
public class ItemStorage : ScriptableObject
{
    public List<ItemBase> Item = new List<ItemBase>();

    public ItemBase GetItemByID(int id)
    {
        if (Item.Find(i => i.itemID == id))
        {
            return Item.Find(i => i.itemID == id);
        }    
        else
        {
            return null;
        }    
    }
}
