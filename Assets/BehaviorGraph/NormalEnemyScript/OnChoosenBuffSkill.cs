using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/OnChoosenBuffSkill")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "OnChoosenBuffSkill", message: "I have chosen buff skill", category: "Events", id: "887a19fa7c64ea63a884ffa95554f840")]
public sealed partial class OnChoosenBuffSkill : EventChannel { }

