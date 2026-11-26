using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static event UnityAction VictoryEvent;
    public static event UnityAction GameOverEvent;

    public static void TriggerVictoryEvent()
    {
        VictoryEvent?.Invoke();
    }

    public static void TriggerGameOverEvent()
    {
        GameOverEvent?.Invoke();
    }
}
