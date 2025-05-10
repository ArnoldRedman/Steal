using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class BasePanel : MonoBehaviour
{
    //找到UI的根节点
    private RectTransform contentRoot;
    //控制淡入淡出的组件 
    private CanvasGroup canvasGroup;
    //淡入淡出速度 
    private float alphaSpeed = 10;
    private bool isShow;
    private Button closeBtn;
    private UnityAction hideAction;

    /// <summary>
    /// 延迟一帧执行刷新面板
    /// </summary>
    /// <returns></returns>
    protected IEnumerator DelayedLayoutUpdate()
    {
        yield return null; // 等待一帧
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentRoot);
    }

    public virtual void Awake()
    {
        contentRoot = GetComponent<RectTransform>();
        closeBtn = this.gameObject.transform.Find("CloseBtn") == null ? null : this.gameObject.transform.Find("CloseBtn").GetComponent<Button>();
        if (closeBtn != null)
        {
            closeBtn.onClick.AddListener(() =>
            {
                Hide();
            });
        }

        // canvasGroup = GetComponent<CanvasGroup>();
        // if (canvasGroup == null) canvasGroup= this.gameObject.AddComponent<CanvasGroup>();
    }

    public virtual void Show()//虚函数 能够被重写  
    {
        this.gameObject.SetActive(true);
    }


    public virtual void Hide()
    {
        this.gameObject.SetActive(false);
        //设置父亲为canvas 要显示的话放到currentShowpanel下面 不显示放回到canvas下 
        this.transform.SetParent(UIManager.Instance.Canvas);
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
