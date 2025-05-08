using UnityEngine.UI;

public class BuyGroundPanel : BasePanel
{
    private Button BuyBtn;

    private void Start()
    {
        BuyBtn = transform.Find("BuyBtn").GetComponent<Button>();
        BuyBtn.onClick.AddListener(BuyCheck);
    }

    private void BuyCheck()
    {
        //判断玩家的金币是否足够
        if (GameManager.instance.CurrPlayerData.Coin - BuildController.Instance.currGround.groundPropertyData.Price >= 0)
        {
            //更改土地状态
            BuildController.Instance.currGround.groundPropertyData.State = 1;
            //减少金币
            GameManager.instance.CurrPlayerData.Coin -= BuildController.Instance.currGround.groundPropertyData.Price;
            //使用事件 触发土地状态变化事件
            EventCenter.Instance.EventTrigger<float>(GameEvent.土地状态变化, 1);
        }
        else
        {
            UIManager.Instance.openPanel<TipPanel>();
        }
    }

}
