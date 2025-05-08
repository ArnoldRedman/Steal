//using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// 资源加载管理器  
/// </summary>
public class ResMgr : SingleTon<ResMgr>
{
    /// <summary>
    /// 资源加载方法
    /// </summary>
    /// <param name="name"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T load<T>(string pathName, Transform father = null) where T : Object  //资源加载类型 游戏对象 非游戏对象 
    {
        T res = Resources.Load<T>(pathName);
        if (res is GameObject) //GameObject 
        {
            T obj = GameObject.Instantiate(res);
            if (father != null)//设置当前对象的父亲为father
            {
                (obj as GameObject).transform.SetParent(father.transform);
                (obj as GameObject).transform.localPosition = Vector3.zero;
                (obj as GameObject).transform.localRotation = Quaternion.identity;
                obj.name = pathName;
            }
            return obj;
        }
        else//AudioClip TextAsset 非游戏对象类型  
        {
            return res;
        }
    }
}
