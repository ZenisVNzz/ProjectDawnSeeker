using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/OnChosenTarget")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "OnChosenTarget", message: "I have chosen Target", category: "Events", id: "bbcb3da90ad025c1d4867e1f05097956")]
public sealed partial class OnChosenTarget : EventChannel { }

