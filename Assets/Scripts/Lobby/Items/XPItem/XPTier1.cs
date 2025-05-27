using UnityEngine;

[CreateAssetMenu(fileName = "XPTier1", menuName = "Item/XPItem/XPTier1")]
public class XPTier1 : ItemBase
{
    public int xpAmount;

    public override int GetXP()
    {
        return xpAmount;
    }
}
