using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel : BasePanel  //激活
{
    public Text TipText;
    void Start()
    {
        
    }
/// <summary>
/// 更新提示框内容 
/// </summary>
    public void UpdateTipText(string text)
    {
        TipText.text = text;    
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
