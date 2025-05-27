using UnityEngine;
using UnityEngine.UI;

public enum ItemType { XPItem }

public class ItemBase : ScriptableObject
{
    public int itemID;
    public string itemName;
    public ItemType itemType;
    public Sprite itemIcon;

    public virtual int GetXP()
    {
        return 0;
    }
}
