using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GeneralDataSave
{
    public int gold;
    public int currentStage;
    public List<CharacterDataSave> characters = new List<CharacterDataSave>();
    public List<ItemDataSave> items = new List<ItemDataSave>();
}
