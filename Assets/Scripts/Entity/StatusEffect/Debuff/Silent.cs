using UnityEngine;

[CreateAssetMenu(fileName = "Silent", menuName = "StatusEffect/Silent")]
public class Silent : StatusEffect
{
    public override void OnApply(CharacterInBattle target)
    {
        target.isSilent = true;
    }
    public override void OnTurn(CharacterInBattle target)
    {
    }
    public override void OnRemove(CharacterInBattle target)
    {
        target.isSilent = false;
    }
}
