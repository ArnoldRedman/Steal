using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IEventInfo
{

}

public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> actions = delegate { };
    public EventInfo(UnityAction<T> action)
    {
        actions += action;
    }
}

public class EventInfo : IEventInfo

{
    public UnityAction actions = delegate { };

    public EventInfo(UnityAction action)
    {
        actions += action;
    }
}

public class EventCenter : MonoBehaviour
{
    public static EventCenter Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    Dictionary<GameEvent, IEventInfo> eventDict = new Dictionary<GameEvent, IEventInfo>();

    //触发事件
    public void EventTrigger(GameEvent gameEvent)
    {
        if (eventDict.ContainsKey(gameEvent))
        {
            (eventDict[gameEvent] as EventInfo).actions?.Invoke();
        }
    }

    public void EventTrigger<T>(GameEvent gameEvent, T value)
    {
        if (eventDict.ContainsKey(gameEvent))
        {
            //Debug.Log(eventDict[gameEvent]);
            (eventDict[gameEvent] as EventInfo<T>).actions?.Invoke(value);
        }
    }


    #region 添加事件监听器
    public void AddEventListener(GameEvent gameEvent, UnityAction action)
    {
        if (eventDict.ContainsKey(gameEvent))
        {
            (eventDict[gameEvent] as EventInfo).actions += action;
        }
        else
        {
            eventDict.Add(gameEvent, new EventInfo(action) as IEventInfo);
        }
    }

    public void AddEventListener<T>(GameEvent gameEvent, UnityAction<T> action)
    {
        if (eventDict.ContainsKey(gameEvent))
        {
            (eventDict[gameEvent] as EventInfo<T>).actions += action;
        }
        else
        {
            eventDict.Add(gameEvent, new EventInfo<T>(action) as IEventInfo);
        }
    }
    #endregion

    #region 移除事件添加器
    public void RemoveEventListener(GameEvent gameEvent, UnityAction action)
    {
        if (eventDict.ContainsKey(gameEvent))
        {
            (eventDict[gameEvent] as EventInfo).actions -= action;
        }
    }

    public void RemoveEventListener<T>(GameEvent gameEvent, UnityAction<T> action)
    {
        if (eventDict.ContainsKey(gameEvent))
        {
            (eventDict[gameEvent] as EventInfo<T>).actions -= action;
        }
    }

    #endregion

    //清空事件
    public void Clear()
    {
        eventDict.Clear();
    }
}
