using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel : BasePanel
{
    public Text tipText;

    public void UpdateTipText(string tip)
    {
        tipText.text = tip;
    }

    private void OnEnable()
    {
        Invoke(nameof(CloseTipPanel),0.8f);
    }


    private void CloseTipPanel()
    {
        UIManager.Instance.closePanel<TipPanel>();
    }
}
