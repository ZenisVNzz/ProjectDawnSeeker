using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Mark", menuName = "StatusEffect/Mark")]
public class Mark : StatusEffect
{
    public static event Action<CharacterRuntime> OnMarkFailed;
    public CharacterRuntime caster;

    public override void OnApply(CharacterRuntime target)
    {
        target.isMark = true;
        target.markCaster = caster;
        MarkHandle markHandle = FindAnyObjectByType<MarkHandle>();
        markHandle.transform.gameObject.SetActive(true);
    }
    public override void OnTurn(CharacterRuntime target)
    {
    }
    public override void OnRemove(CharacterRuntime target)
    {
        target.isHeatShock = false;
        if (caster.isAlive && caster != null)
        {
            OnMarkFailed?.Invoke(caster);
            MarkHandle markHandle = FindAnyObjectByType<MarkHandle>();
            markHandle.transform.gameObject.SetActive(false);
        }  
        else
        {
            Debug.LogWarning("Caster is not alive or is null.");
        }    
    }
}
