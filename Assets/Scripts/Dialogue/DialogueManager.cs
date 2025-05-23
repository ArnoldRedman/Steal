using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 对话管理器
/// </summary>
public class DialogueManager : UnitySingleTon<DialogueManager>
{
    [Header("当前NPC")] 
    public DialogueItem currNPC;

    private void Update()
    {
        //判断是否在UI上
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.collider.CompareTag("NPC"))
            {
               //设置当前NPC
               currNPC = hit.collider.gameObject.GetComponent<DialogueItem>();
               //隐藏土块选择提示
               BuildController.Instance.DisAppareCurrSelectedTip();
            }
            else
            {
                currNPC = null;
            }
        }

        //点击当前NPC
        if (Input.GetMouseButtonDown(0) && currNPC != null)
        {
            //打开对话面板
            UIManager.Instance.openPanel<DialoguePanel>().UpdateNextDialogue(currNPC.id);
        }
    }
}
