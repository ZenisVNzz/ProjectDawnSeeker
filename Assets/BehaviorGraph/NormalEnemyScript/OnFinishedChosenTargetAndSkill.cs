using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/OnFinishedChosenTargetAndSkill")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "OnFinishedChosenTargetAndSkill", message: "I'm done choosing target and skill", category: "Events", id: "dc1b786883871cca3d4ba3f7d85df0ad")]
public sealed partial class OnFinishedChosenTargetAndSkill : EventChannel { }

