using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
/// <summary>
/// 场景管理器  
/// </summary>
public class SceneMgr : UnitySingleTonMono<SceneMgr>
{
    /// <summary>
    /// 同步切换场景 
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName, UnityAction fun = null)
    {
        SceneManager.LoadScene(sceneName);
        fun?.Invoke();
    }
    /// <summary>
    /// 异步加载场景的方法  
    /// </summary>  
    /// <param name="sceneName"></param>
    /// <param name="fun"></param>
    public void LoadSceneAsync(string sceneName, UnityAction fun = null)
    {
        StartCoroutine(LoadSceneEnumerator(sceneName, fun));

    }
    //加载游戏场景的协程
    private IEnumerator LoadSceneEnumerator(string sceneName, UnityAction fun = null)//场景的名字  加载完场景之后要做的事情
    {
        //获取加载进度  
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);
        float time = 0;
        float progress = 0;
        while (time <= 3f)//实际做的时候 要用判断ao.progress进度值 如果进度值是为1说明加载完了
        {
            time += 0.1f;
            progress = time / 3f;
            EventCenter.Instance.EventTrigger<float>(GameEvent.进度条加载, progress);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        //场景加载结束了 要执行委托函数
        fun?.Invoke();
    }


}
