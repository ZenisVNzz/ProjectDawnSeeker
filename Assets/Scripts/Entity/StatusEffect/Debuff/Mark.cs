using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Mark", menuName = "StatusEffect/Mark")]
public class Mark : StatusEffect
{
    public static event Action<CharacterInBattle> OnMarkFailed;
    public CharacterInBattle caster;

    public override void OnApply(CharacterInBattle target)
    {
        target.isMark = true;
        target.markCaster = caster;
        MarkHandle markHandle = FindAnyObjectByType<MarkHandle>();
        markHandle.transform.gameObject.SetActive(true);
    }
    public override void OnTurn(CharacterInBattle target)
    {
    }
    public override void OnRemove(CharacterInBattle target)
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
