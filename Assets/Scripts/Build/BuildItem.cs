using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 每一项建造物品的面板
/// </summary>
public class BuildItem : MonoBehaviour
{
    public Image sprite;
    public Text nameText;
    public Text price;
    public BuildItemData CurrData;
    private Button buildBtn;

    private void Start()
    {
        buildBtn = gameObject.GetComponent<Button>();
        if (buildBtn!=null)
        {
            buildBtn.onClick.AddListener(BuyCheck);
        }
    }

    /// <summary>
    /// 购买建造物的逻辑
    /// </summary>
    private void BuyCheck()
    {
        //判断玩家金币是否足够购买
        if (GameManager.instance.CurrPlayerData.Coin>=CurrData.price)
        {
            GameManager.instance.CurrPlayerData.Coin-=CurrData.price;
            //克隆物品到土地上
            GameObject obj = ResMgr.Instance.load<GameObject>($"Ground/{CurrData.prefab}",BuildController.Instance.currGround.transform);
            obj.name = "Building";//统一命名为Building，可以通过这个名字拿到相应组件
            obj.transform.localScale = Vector3.one;
            EventCenter.Instance.EventTrigger(GameEvent.建造物品成功);//建造物品成功
            //更改土地状态为2
            BuildController.Instance.currGround.groundPropertyData.State = 2;
            EventCenter.Instance.EventTrigger<float>(GameEvent.土地状态变化,2);
        }
        else
        {
            UIManager.Instance.openPanel<TipPanel>().UpdateTipText("金币不足");
        }
    }


    public void UpdateData(BuildItemData data)
    {
        CurrData = data;
        nameText.text = data.name;
        price.text = data.price.ToString();
        sprite.sprite = ResMgr.Instance.load<Sprite>($"Sprite/{data.sprite}");
    }
}
