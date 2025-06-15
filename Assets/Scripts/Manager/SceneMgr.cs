using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
/// <summary>
/// 场景管理器  
/// </summary>
public class SceneMgr:UnitySingleTonMono<SceneMgr>
{
    /// <summary>
    /// 同步切换场景 
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName,UnityAction fun=null)
    {
        SceneManager.LoadScene(sceneName);
        fun?.Invoke();  
    }
/// <summary>
/// 异步加载场景的方法  
/// </summary>  
/// <param name="sceneName"></param>
/// <param name="fun"></param>
    public void LoadSceneAsync(string sceneName,UnityAction fun=null)
    {
        StartCoroutine(LoadSceneEnumerator(sceneName,fun));
        
    }

    private IEnumerator LoadSceneEnumerator(string sceneName,UnityAction fun=null)
    {
        UIManager.Instance.openPanel<LoadPanel>();
        //获取加载进度  
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);
        float time = 0; 
        float progress = 0;
        while (ao.progress < 1)
        {
            // time += 0.1f;
            // progress = time/3f;
            progress = ao.progress;
            EventCenter.Instance.EventTrigger<float>(GameEvent.进度条加载,progress);
            yield return null;
            // yield return new WaitForSeconds(Time.deltaTime);
        }
        
        fun?.Invoke();  
    }
    
    
}
