using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class TargetArrowManager : MonoBehaviour
{
    public List<LineRenderer> lineRenderers;
    public BattleManager battleManager;
    public List<TargetArrow> targetArrow;

    public void TurnOffArrow()
    {
        for (int i = 0; i < battleManager.TeamPlayer.Count; i++)
        {
            lineRenderers[i].enabled = false;     
        }
        foreach (TargetArrow arrow in targetArrow)
        {
            Destroy(arrow.arrowInstances);
        }
    }
}
