using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 输入管理器 基本的按键控制  
/// </summary>
public class InputManager : UnitySingleTon<InputManager>
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UIManager.Instance.openPanel<GamePausePanel>();
        }
    }
}
