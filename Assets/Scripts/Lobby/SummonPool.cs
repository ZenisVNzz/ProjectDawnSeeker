using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="NewSummonPool", menuName ="SummonPool")]
public class  SummonPool : ScriptableObject
{
    public List<CharacterData> CharacterPool;
}
