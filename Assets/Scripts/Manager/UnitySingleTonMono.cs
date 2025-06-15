using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitySingleTonMono<T> : MonoBehaviour where T : MonoBehaviour //限制T的类型必须是MonoBehaviour的派生类  
{
    private static bool isQuitting;
    private static T instance; //来存储当前的单例 

    public static T Instance
    {
        get
        {
            if (isQuitting) return null;
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    //new 一个单例对象  
                    var obj = new GameObject();
                    obj.name = typeof(T).Name;
                    instance = (T)obj.AddComponent<T>();
                    DontDestroyOnLoad(obj);
                }
            }
            return instance;
        }
    }

    public virtual void Awake()
    {
        
        if (instance == null)
        {
            instance = this as T;
            this.name = typeof(T).Name;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            GameObject.DestroyImmediate(this.gameObject);
        }
    }

    private void OnApplicationQuit() => isQuitting = true;
}

public class UnitySingleTon<T> : MonoBehaviour where T : MonoBehaviour //限制T的类型必须是MonoBehaviour的派生类  
{
    private static bool isQuitting;
    private static T instance; //来存储当前的单例 

    public static T Instance
    {
        get
        {
            if (isQuitting) return null;
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    //new 一个单例对象  
                    var obj = new GameObject();
                    obj.name = typeof(T).Name;
                    instance = (T)obj.AddComponent<T>();
                }
            }

            return instance;
        }
    }

    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            this.name = typeof(T).Name;
        }
        else
        {
            GameObject.DestroyImmediate(this.gameObject);
        }
    }

    private void OnApplicationQuit() => isQuitting = true;
}