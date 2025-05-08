using UnityEngine;

public class UnitySingleTonMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;//脚本对象  

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                //创建一个新的游戏对象  
                GameObject obj = new GameObject();
                instance = obj.AddComponent<T>();
                obj.name = typeof(T).Name;
            }
            return instance;
        }
    }
    /// <summary>
    ///虚函数 表示可以被重写
    /// </summary>
    public virtual void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
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
}
