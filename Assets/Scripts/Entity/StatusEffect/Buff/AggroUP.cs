using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "AggroUP", menuName = "StatusEffect/AggroUP")]
public class AggroUP : StatusEffect
{
    public CharacterRuntime provocateur;

    public override void OnApply(CharacterRuntime target)
    {
    }
    public override void OnTurn(CharacterRuntime target)
    {
    }
    public override void OnRemove(CharacterRuntime target)
    {
    }
    public override CharacterRuntime Getprovocateur()
    {
        return provocateur;
    }
}
