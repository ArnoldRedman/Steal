using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 保存存档面板 
/// </summary>
public class SaveFilePanel : BasePanel
{
    [Header("保存存档按钮")]
    public Button saveFileBtn;   
    [Header("取消存档按钮")]
    public Button cancelBtn;    
    [Header("文档名")]
    public InputField saveFileInput;

    private void OnEnable()
    {
        saveFileBtn.onClick.AddListener(saveFile);
        cancelBtn.onClick.AddListener(cancel);
    }

    private void OnDisable()
    {
        saveFileBtn.onClick.RemoveListener(saveFile);
        cancelBtn.onClick.RemoveListener(cancel);   
    }

    private void cancel()
    {
        UIManager.Instance.closePanel<SaveFilePanel>();
    }

    /// <summary>
/// 保存文档
/// </summary>
    private void saveFile()
    {
        string fileName=saveFileInput.text.Trim()==""?"新加存档":saveFileInput.text.Trim();
        try
        {
            GameManager.Instance.SaveArchive(ArchiveType.新游戏存档, fileName);
            UIManager.Instance.closePanel<SaveFilePanel>();    
            UIManager.Instance.openPanel<TipPanel>().UpdateTipText("成功存档");
        }
        catch (Exception e)
        {
            UIManager.Instance.openPanel<TipPanel>().UpdateTipText("保存失败");
        }
    }
}
