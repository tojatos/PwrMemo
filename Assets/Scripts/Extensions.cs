using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public static class Extensions
{
    public static void AddTrigger(this GameObject gameObject, EventTriggerType eventTriggerType, UnityAction onTrigger) =>
        gameObject.AddTrigger(eventTriggerType, e => onTrigger());
    public static void AddTrigger(this GameObject gameObject, EventTriggerType eventTriggerType, UnityAction<BaseEventData> onTrigger)
    {
        EventTrigger trigger = gameObject.GetComponent<EventTrigger>() ?? gameObject.AddComponent<EventTrigger>();
        var entry = new EventTrigger.Entry {eventID = eventTriggerType};
        entry.callback.AddListener(onTrigger);
        trigger.triggers.Add(entry);
    }
    
    private static System.Random rng = new System.Random();  
    
    public static void Shuffle<T>(this IList<T> list)  
    {  
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            T value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }  
    }
}