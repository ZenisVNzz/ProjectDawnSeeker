using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "ShoudISaveMP", story: "I should save MP, check [ChosenTarget] and [MySelf]", category: "Conditions", id: "f6c7c314ca53cf5377a13c03b7a848f4")]
public partial class ShoudISaveMpCondition : Condition
{
    [SerializeReference] public BlackboardVariable<CharacterRuntime> ChosenTarget;
    [SerializeReference] public BlackboardVariable<CharacterRuntime> MySelf;

    public override bool IsTrue()
    {
        if (ChosenTarget.Value.currentHP <= ChosenTarget.Value.HP * 0.1f)
        {
            return true;
        }    
        if (MySelf.Value.currentHP <= MySelf.Value.HP * 0.42f)
        {
            return false;
        }
        return true;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
