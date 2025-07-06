using UnityEngine;

[CreateAssetMenu(fileName = "NewHeatShock", menuName = "StatusEffect/HeatShock")]
public class HeatShock : StatusEffect
{
    public override void OnApply(CharacterRuntime target)
    {
        target.isHeatShock = true;
    }
    public override void OnTurn(CharacterRuntime target)
    {
    }
    public override void OnRemove(CharacterRuntime target)
    {
        target.isHeatShock = false;
    }
}
