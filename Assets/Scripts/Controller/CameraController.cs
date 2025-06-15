using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [Header("相机移动的速度")]
    public float MoveSpeed;
    [Header("水平方向的阈值")]
    public float MX;
    [Header("垂直方向的阈值")]
    public float MY;
    [Header("相机放大的最大值")]
    public float MaxW;
    [Header("相机缩小的最小值")]
    public float MinW;
    [Header("相机放大缩小的速度")]
    public float WheelSpeed;
    private Vector3 OriginPos;
    private Quaternion OriginRot;
    private Camera cam;
    void Start()
    {
        cam = GetComponent<Camera>();
        OriginPos=this.transform.position;
        OriginRot = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) //如果在UI层上就不进行移动
        {
            return;
        }

        if (cam == null) return;
        //拿到水平和垂直的偏移量 
        float MoveX = Input.GetAxis("Horizontal");
        float MoveY = Input.GetAxis("Vertical");
        //获取滚轮的偏移值 
        float Wheel = Input.GetAxis("Mouse ScrollWheel");
        
        //拿到新位置    
        Vector3 newPos = new Vector3(transform.position.x +MoveX,transform.position.y,transform.position.z+MoveY);
        //计算field的值   
        float newView=cam.fieldOfView-Wheel*30;
        //判断移动区域  
        if (newPos.x-OriginPos.x>=MX)
        {
            newPos.x = OriginPos.x + MX;
        }else if (newPos.x - OriginPos.x <= -MX)
        {
            newPos.x=OriginPos.x - MX;
        }

        if (newPos.z-OriginPos.z>=MY)
        {
            newPos.z=OriginPos.z + MY;
        }else if (newPos.z - OriginPos.z <= -MY)
        {
            newPos.z=OriginPos.z - MY;
        }
        //判断放大缩小   
        if (newView>=MaxW)
        {
            newView=MaxW;
        }else if (newView <= MinW)
        {
            newView=MinW;
        }
        
        
        //使用lerp更新位置  
        transform.position = Vector3.Lerp(transform.position,newPos,Time.deltaTime*MoveSpeed);
        //更新 fieldOfView的值 
        cam.fieldOfView=Mathf.Lerp(cam.fieldOfView,newView,Time.deltaTime*WheelSpeed);









    }
}
