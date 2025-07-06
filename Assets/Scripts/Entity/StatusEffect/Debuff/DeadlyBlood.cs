using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "DeadlyBlood", menuName = "StatusEffect/DeadlyBlood")]
public class DeadlyBlood : StatusEffect
{
    public override void OnApply(CharacterRuntime target)
    {
        target.DecreaseATK(2.5f);
        target.DecreaseDEF(2.5f);
    }
    public override void OnTurn(CharacterRuntime target)
    {
    }
    public override void OnRemove(CharacterRuntime target)
    {    
        target.IncreaseATK(2.5f);
        target.IncreaseDEF(2.5f);
    }
}
