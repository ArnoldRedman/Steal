using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 任务面板基类 点击有下拉效果
/// </summary>
public class Task : MonoBehaviour
{
    [Header("任务下拉按钮")] public Button taskBtn;
    [Header("任务描述")] public Text taskBtnText;
    [Header("任务下拉列表")] public GameObject taskScrollView;
    [Header("任务容器")] public GameObject taskContent;
    [Header("任务列表对象")] public GameObject taskView;
    [Header("指示箭头")] public Image arrowImage;
    
    //任务列表是否打开
    private bool isOpen;
    //任务列表Y的长度
    private int contentLength;
    //任务类型
    [HideInInspector] public string taskType;
    private bool isAnimating; // 新增：动画状态锁


    public virtual void Start()
    {
        //OpenTaskContent();
        // 初始化状态
        isOpen = false;
        arrowImage.transform.rotation = Quaternion.Euler(0, 0, 90);
        taskView.transform.localScale = Vector3.zero;
        taskScrollView.transform.localScale = new Vector3(1, 0, 1);
        OpenTaskContent();
        
        taskBtn.onClick.AddListener(OpenTaskContent);
    }

    /// <summary>
    /// 打开任务详情面板
    /// </summary>
    public virtual void OpenTaskContent()
    {
        if (isAnimating) return; // 防止动画过程中重复点击
        
        isAnimating = true;
        isOpen = !isOpen;
        
        // 箭头旋转动画
        arrowImage.transform.DORotate(
            new Vector3(0, 0, isOpen ? 0 : 90),
            0.3f
        );

        if (isOpen)
        {
            OpenAnimation();
        }
        else
        {
            CloseAnimation();
        }
    }

    private void OpenAnimation()
    {
        // 先展开容器
        taskScrollView.transform.DOScaleY(1, 0.5f)
            .OnComplete(() => {
                // 再显示内容
                taskView.transform.DOScale(Vector3.one, 0.3f)
                    .OnComplete(() => {
                        isAnimating = false;
                        EventCenter.Instance.EventTrigger(GameEvent.任务UI切换);
                    });
            });
    }
    private void CloseAnimation()
    {
        // 先隐藏内容
        taskView.transform.DOScale(Vector3.right, 0.3f)
            .OnComplete(() => {
                // 再收起容器
                taskScrollView.transform.DOScaleY(0, 0.5f)
                    .OnComplete(() => {
                        isAnimating = false;
                        EventCenter.Instance.EventTrigger(GameEvent.任务UI切换);
                    });
            });
    }

    /// <summary>
    /// 更新任务项的方法 
    /// 当创建新任务的时候 会执行任务开始的事件 这时候会执行这个方法
    /// 根据传进来的任务id来添加对应的任务项
    /// </summary>
    /// <param name="id">更新任务id</param>
    public void UpdateTaskItem(string id)
    {
        //拿到任务数据
        TaskItemData taskItemData = GameManager.instance.taskItemDict[id];
        if (taskItemData == null)
        {
            return;
        }
        //如果是支线任务 就显示领取任务成功的提示
        if (taskItemData.id != "0" && taskItemData.type == "支线任务")
        {
            UIManager.Instance.openPanel<TipPanel>().UpdateTipText("领取任务成功");
        }
        //判断任务类型 然后显示对应的任务项
        if (taskItemData.type == taskType)
        {
            //克隆对应的taskItem 到taskContent下
            GameObject obj = ResMgr.Instance.load<GameObject>("UI/taskItem",taskContent.transform);
            obj.transform.localScale = Vector3.one;
            obj.GetComponent<taskItem>().UpdateTaskItem(taskItemData.id);
            //判断任务类型 添加到对应的字典中
            if (taskItemData.type == "主线任务")
            {
                TaskManager.Instance.mainTaskItemDict.Add(taskItemData.id, obj.GetComponent<taskItem>());
            }

            if (taskItemData.type == "支线任务")
            {
                TaskManager.Instance.branchTaskItemDict.Add(taskItemData.id, obj.GetComponent<taskItem>());
            }
        }
    }
}
