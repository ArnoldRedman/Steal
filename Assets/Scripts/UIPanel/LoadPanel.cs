//using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine.UI;


public class LoadPanel : BasePanel
{

    private Slider slider;
    private UnityAction<float> SetValue;
    public override void Awake()
    {
        base.Awake();
        slider = this.transform.Find("Slider").GetComponent<Slider>();
        SetValue += setSliderValue;
        EventCenter.Instance.AddEventListener<float>(GameEvent.进度条加载, SetValue);
    }
    public void setSliderValue(float value)//更新进度条
    {
        slider.value = value;
        print(value);
    }


    void Start()
    {

    }



    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener(GameEvent.进度条加载, SetValue);
    }
}
