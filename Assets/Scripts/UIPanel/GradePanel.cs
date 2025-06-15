using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GradePanel : BasePanel
{
    [Header("等级图标")]
    public Image gradeIcon;
    [Header("等级描述")]
    public Text gradeDes;
    private void OnEnable()
    {
        UpdateGrade();
        ShowAnim(1f,UIAnimaEvent.渐变,()=>{UIManager.Instance.closePanel<GradePanel>();});
    }

    private void OnDisable()
    {
        HideAnim();
    }
/// <summary>
/// 更新面板内容
/// </summary>
    public void UpdateGrade()
    {
        int level = GameManager.Instance.playerData.GameLevel;//获取当前游戏等级 
        gradeIcon.enabled=true;
        gradeDes.enabled = true;
        switch (level)
        {
            case 1:
                gradeIcon.sprite = ResMgr.Instance.load<Sprite>("Icon/等级一");
                gradeDes.text = "恭喜你达到等级一";
                break;
            case 2:
                gradeIcon.sprite = ResMgr.Instance.load<Sprite>("Icon/等级二");
                gradeDes.text = "恭喜你达到等级二";
                break;
            case 3:
                gradeIcon.sprite = ResMgr.Instance.load<Sprite>("Icon/等级三");
                gradeDes.text = "恭喜你达到等级三";
                break;
            default:
                gradeIcon.enabled=false;
                gradeDes.enabled=false;
                break;
            
        }
    }
    
}
