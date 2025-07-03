using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/OnCharging")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "OnCharging", message: "I'm Charging", category: "Events", id: "914cbc9821d143151525e29fc2e42667")]
public sealed partial class OnCharging : EventChannel { }

