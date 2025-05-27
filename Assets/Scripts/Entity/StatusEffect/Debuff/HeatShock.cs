using UnityEngine;

[CreateAssetMenu(fileName = "NewHeatShock", menuName = "StatusEffect/HeatShock")]
public class HeatShock : StatusEffect
{
    public override void OnApply(CharacterInBattle target)
    {
        target.isHeatShock = true;
    }
    public override void OnTurn(CharacterInBattle target)
    {
    }
    public override void OnRemove(CharacterInBattle target)
    {
        target.isHeatShock = false;
    }
}
