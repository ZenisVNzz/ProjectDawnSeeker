using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBanner", menuName = "Banner")]
public class Banner : ScriptableObject
{
    public List<CharacterData> rateUpCharacter;
    public List<CharacterData> poolCharacter;
}
