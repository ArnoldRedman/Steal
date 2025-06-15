using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductItem : MonoBehaviour
{
    public Image Icon;
    public Text name;
    public Text num;
    void Start()
    {
        
    }
/// <summary>
/// 更新物品的信息 
/// </summary>
/// <param name="id"></param>
/// <param name="count"></param>
    public void UpdateData(string id,int count)
    {
        //拿到物品的信息 
        ProductItemData productItemData = GameManager.Instance.productItemDict[id];
        Icon.sprite = Resources.Load<Sprite>("Icon/" +productItemData.sprite);
        name.text = productItemData.name;
        num.text=count.ToString();  
    }
    void Update()
    {
        
    }
}
