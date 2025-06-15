using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyGroundPanel : BasePanel
{
    private Button BuyBtn;
    private Button CloseBtn;
    void Start()
    {
        BuyBtn=transform.Find("BuyBtn").GetComponent<Button>();
        CloseBtn=transform.Find("CloseBtn").GetComponent<Button>();
        BuyBtn.onClick.AddListener(buyGround);
        CloseBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.closePanel<BuyGroundPanel>();
        });
    }
/// <summary>
/// 购买土地  
/// </summary>
    private void buyGround() //把当前选中的土地的状态要切换掉  
    {
        //判断是否能购买  
        if (GameManager.Instance.playerData.Coin>=BuildController.Instance.currentGround.groundProperty.Price)
        {
            //能够购买 改变土地状态   
            BuildController.Instance.currentGround.groundProperty.State = 1;
            GameManager.Instance.playerData.Coin-=BuildController.Instance.currentGround.groundProperty.Price;
            //事件  土地状态改变可能会带来一些变化   
            EventCenter.Instance.EventTrigger<int>(GameEvent.土地状态变化,1);
            UIManager.Instance.closePanel<BuyGroundPanel>();
        }
        else
        {
            UIManager.Instance.openPanel<TipPanel>().UpdateTipText("金币不够");
        }
    }


}
