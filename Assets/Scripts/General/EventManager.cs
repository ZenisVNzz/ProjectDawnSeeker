using System;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public static class EventManager
{
    private static Dictionary<string, Action> eventTable = new Dictionary<string, Action>();

    public static void Subscribe(string eventName, Action callBack)
    {
        if (!eventTable.ContainsKey(eventName))
        {
            eventTable[eventName] = callBack;
        }
        else
        {
            eventTable[eventName] += callBack;
        }
    }

    public static void Unsubscribe(string eventName, Action callBack)
    {
        if (eventTable.ContainsKey(eventName))
        {
            eventTable[eventName] -= callBack;
        }              
    }

    public static void Call(string eventName)
    {
        if(eventTable.TryGetValue(eventName, out Action callBack))
        {
            callBack?.Invoke();
        }    
    }
}
