using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
public class BasePanel : MonoBehaviour
{
    private RectTransform rootRect;
    //关闭按钮  并不是所有面板都有的 
    private Button closeBtn;    
    
    //控制淡入淡出的组件 
    private CanvasGroup canvasGroup;
    //淡入淡出速度 
    private float alphaSpeed=10;
    private bool isShow;
    private UnityAction hideAction;
    
    protected IEnumerator DelayedLayoutUpdate()
    {
        yield return null; // 等待一帧
        LayoutRebuilder.ForceRebuildLayoutImmediate(rootRect);
    }

    public virtual void Awake()
    {
        rootRect = this.transform as RectTransform;
        closeBtn=transform.Find("CloseBtn")==null?null:transform.Find("CloseBtn").GetComponent<Button>();
        if (closeBtn != null)
        {
            closeBtn.onClick.AddListener(()=>{Hide();});
        }
     
    }

 
    public virtual void OnDestroy()
    {
        if (closeBtn == null) return;
        closeBtn.onClick.RemoveAllListeners();
    }

    /// <summary>
/// 显示动画
/// </summary>
/// <param name="ev">ui动画类型</param>
    public void ShowAnim(float time=0.5f,UIAnimaEvent ev=UIAnimaEvent.渐变, TweenCallback action=null)
    {
        if (ev == UIAnimaEvent.渐变)
        {
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null) canvasGroup= this.gameObject.AddComponent<CanvasGroup>();//添加canvasGroup组件来控制渐变效果  
            canvasGroup.alpha = 0;
            canvasGroup.DOFade(1, time).OnComplete(action);
        }
    }
/// <summary>
/// 隐藏动画
/// </summary>
/// <param name="ev">ui动画类型</param>
    public void HideAnim(float time=0.5f, UIAnimaEvent ev=UIAnimaEvent.渐变, TweenCallback action=null)
    {
        if (ev == UIAnimaEvent.渐变)
        {
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null) canvasGroup= this.gameObject.AddComponent<CanvasGroup>();//添加canvasGroup组件来控制渐变效果  
            canvasGroup.alpha = 1;
            canvasGroup.DOFade(0, time).OnComplete(action);
        }
        else
        {
            action();
        }
    }
    public virtual void Show(float time=0.5f, UIAnimaEvent ev=UIAnimaEvent.默认, TweenCallback action=null)//虚函数 能够被重写    等下UI管理器要更改一下逻辑  
    {
        this.gameObject.SetActive(true);
        if (ev == UIAnimaEvent.渐变)
        {
            ShowAnim();
        }
    }


    public virtual void Hide()
    {
            HideAnim(0.5f,UIAnimaEvent.默认, () =>
            {
                this.gameObject.SetActive(false);
                this.transform.SetParent(UIManager.Instance.Canvas);
            
            });


    }

 

    private void Update()
    {
        // //淡入
        // if (isShow&&canvasGroup.alpha!=1)
        // {
        //     canvasGroup.alpha += alphaSpeed*Time.deltaTime;
        //     if (canvasGroup.alpha>=1)
        //     {
        //         canvasGroup.alpha = 1;
        //     }
        // }
        // else//淡出
        // {
        //     canvasGroup.alpha -= alphaSpeed*Time.deltaTime;
        //         print(canvasGroup.alpha);
        //     if (canvasGroup.alpha<=0)
        //     {
        //         canvasGroup.alpha = 0;
        //         //删除面板  
        //         hideAction?.Invoke();
        //     }
        // }
    }
}
