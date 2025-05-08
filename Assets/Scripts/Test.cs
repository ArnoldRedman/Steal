using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        // 确保使用 AddEventListener<string> 而非非泛型的 AddEventListener
        //EventCenter.Instance.AddEventListener<string>(GameEvent.游戏失败, updateTIme);
    }

    private void updateTIme(string value)
    {
        Debug.Log(string.Format("游戏失败，当前时间为{0}", value));
    }

    private void OnDestroy()
    {
        // 确保 EventCenter 实例未销毁
        //if (EventCenter.Instance != null)
        //{
        //    EventCenter.Instance.RemoveEventListener<string>(GameEvent.游戏失败, updateTIme);
        //}
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //PoolMgr.Instance.getObj("Cube");
        }
    }
}
