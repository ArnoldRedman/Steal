using UnityEngine;
using UnityEngine.EventSystems;

//建造模块
public class BuildController : UnitySingleTon<BuildController>
{
    [HideInInspector] public GroundProperties currGround;
    public GameObject currSelectedTip;

    private void Start()
    {
        EventCenter.Instance.AddEventListener<float>(GameEvent.土地状态变化, StateChange);
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener<float>(GameEvent.土地状态变化, StateChange);
    }

    //土地状态变化
    private void StateChange(float value)
    {
        switch (value)
        {
            case 0://隐藏购买的提示物体

                break;

            case 1://已经购买，已经购买的提示物体，隐藏InitPrefab
                if (currGround.HasBuyObj == null)
                {
                    GameObject hasBuyObj = ResMgr.Instance.load<GameObject>("HasBuyTip", currGround.transform);
                    currGround.HasBuyObj = hasBuyObj;
                    hasBuyObj.transform.localScale = Vector3.one * 0.3f;
                    hasBuyObj.transform.localPosition = Vector3.up * 0.5f;
                }
                currGround.HasBuyObj.SetActive(true);
                //隐藏InitPrefab
                currGround.InitPrefab.SetActive(false);
                currGround.groundPropertyData.isShowInitPrefab = false;
                //隐藏购买窗口
                UIManager.Instance.closePanel<BuyGroundPanel>();
                break;

            case 2://已经建造了，隐藏已购买的提示物品
                GameObject obj = currGround.transform.Find("HasBuyTip").gameObject;
                if (obj!=null)
                {
                    Destroy(obj);
                }
                break;
        }
    }

    private void Update()
    {
        //判断是否在UI层上，为true直接return 不去执行射线的碰撞检测
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                currSelectedTip.SetActive(true);
                currGround = hit.collider.GetComponent<GroundProperties>();
                //Debug.Log(currGround.groundPropertyData.GroundName);
                //显示鼠标悬浮的提示框
                currSelectedTip.transform.position = hit.collider.transform.position;
                currSelectedTip.transform.localScale = hit.collider.transform.localScale;
            }
        }

        //鼠标左键点击，弹出相应的UI
        if (Input.GetMouseButtonDown(0) && currGround != null)
        {
            switch (currGround.groundPropertyData.State)
            {
                case 0://未购买 显示购买窗口
                    UIManager.Instance.openPanel<BuyGroundPanel>();
                    break;

                case 1://已购买 显示建造窗口
                    UIManager.Instance.openPanel<BuildPanel>();
                    break;

                case 2://已建造 显示建造物详情 如果是商店要区分普通建造物
                    string type = currGround.transform.GetComponentInChildren<BuildItemBase>().buildType;
                    if (type == "other")//显示商店详情页面
                    {
                        UIManager.Instance.closePanel<BuildItemDetailPanel>();
                        UIManager.Instance.closePanel<ShopDetailPanel>();
                        UIManager.Instance.openPanel<ShopDetailPanel>();
                    }
                    else
                    {
                        UIManager.Instance.closePanel<ShopDetailPanel>();
                        UIManager.Instance.closePanel<BuildItemDetailPanel>();
                        UIManager.Instance.openPanel<BuildItemDetailPanel>();
                    }
                    break;
            }
        }
    }
    
    /// <summary>
    /// 提供给外部隐藏提示土地的方法
    /// </summary>
    public void DisAppareCurrSelectedTip()
    {
        currGround = null;
        currSelectedTip.SetActive(false);
    }
}
