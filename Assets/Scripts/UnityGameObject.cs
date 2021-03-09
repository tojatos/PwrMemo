using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public static class UnityGameObject
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
}