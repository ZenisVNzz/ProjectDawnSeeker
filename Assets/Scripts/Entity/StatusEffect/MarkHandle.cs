using UnityEngine;

public class MarkHandle : MonoBehaviour
{
    public StatusEffect defDown;

    private void OnEnable()
    {
        Mark.OnMarkFailed += HandleMarkFailed;
    }

    private void OnDisable()
    {
        Mark.OnMarkFailed -= HandleMarkFailed;
    }

    private void HandleMarkFailed(CharacterRuntime caster)
    {
        caster.ApplyStatusEffect(defDown, 2);
    }
}

