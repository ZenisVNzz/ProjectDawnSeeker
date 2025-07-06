using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "DoIHaveMuchMpLeft", story: "Do I have much MP left, check [MySelf]", category: "Conditions", id: "b65bf81b63be2595a6ace2065711605c")]
public partial class DoIHaveMuchMpLeftCondition : Condition
{
    [SerializeReference] public BlackboardVariable<CharacterInBattle> MySelf;

    public override bool IsTrue()
    {
        float currentMp = MySelf.Value.currentMP;
        if (currentMp >= MySelf.Value.MP * 0.8f)
        {
            return true; 
        }
        else
        {
            return false;
        }
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
