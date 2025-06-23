using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GeneralDataSave
{
    public int gold;
    public int currentStage;
    public bool isCompletedTarvenTutorial;
    public bool isCompletedManageTutorial;
    public bool isCompletedBattleTutorial;
    public List<CharacterDataSave> characters = new List<CharacterDataSave>();
    public List<int> equipedChar = new List<int>();
    public List<ItemDataSave> items = new List<ItemDataSave>();
}
