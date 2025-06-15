using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;   
/// <summary>
/// 加载存档界面
/// </summary>
public class LoadFilePanel : BasePanel
{
    [Header("容器")]
    public GameObject content;

    private void OnEnable()
    {
        Init();
    }

    private void OnDisable()
    {
        
    }

    /// <summary>
/// 初始化存档   
/// </summary>
    private void Init()
    {
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
        //克隆新的  拿到archive文件夹下面的所有子文件夹的名字  
        string parentPath=Application.persistentDataPath+"/Archive/EveryGame";
        string lastGamePath=Application.persistentDataPath+"/Archive/LastGame";
        if(Directory.Exists(lastGamePath)) UpdateNewFileItem(lastGamePath);
        if(!Directory.Exists(parentPath))return;
        string[] allPath = Directory.GetDirectories(parentPath);
        foreach (var path in allPath)
        {
            UpdateNewFileItem(path);
        }   
            
    }
/// <summary>
/// 更新存档项
/// </summary>
/// <param name="path"></param>
    public void UpdateNewFileItem(string path)
    {
        MainSceneData data = JsonMgr.Instance.LoadData<MainSceneData>("main", path);
        GameObject obj = ResMgr.Instance.load<GameObject>("UI/ArchiveFileItem",content.transform);
        obj.GetComponent<ArchiveFileItem>().UpdateData(data);
    }    

    // Update is called once per frame
    void Update()
    {
        
    }
}
