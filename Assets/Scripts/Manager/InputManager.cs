using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : UnitySingleTon<InputManager>
{
    //删除内容：用于Inspector中显示中文不乱码

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.openPanel<GamePausePanel>();
        }
    }
}
