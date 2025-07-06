using UnityEngine;

[CreateAssetMenu(fileName = "Silent", menuName = "StatusEffect/Silent")]
public class Silent : StatusEffect
{
    public override void OnApply(CharacterRuntime target)
    {
        target.isSilent = true;
    }
    public override void OnTurn(CharacterRuntime target)
    {
    }
    public override void OnRemove(CharacterRuntime target)
    {
        target.isSilent = false;
    }
}
