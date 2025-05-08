using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("相机的移动速度")]
    public int moveSpeed;
    public int WheelSpeed;

    //相机移动范围
    private float MaxX;
    private float MaxZ;

    private Vector3 originalPos;
    private Camera currCamera;

    private void Start()
    {
        originalPos = transform.position;
        currCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        //计算出相机的新的位置
        MaxX = transform.position.x + moveX;
        MaxZ = transform.position.z + moveZ;

        //限制相机的范围
        MaxX = Mathf.Clamp(MaxX, -20f, 20f);
        MaxZ = Mathf.Clamp(MaxZ, -20f, 20f);
        Vector3 newPos = new Vector3(MaxX, transform.position.y, MaxZ);

        //获取鼠标滚轮

        float Wheel = Input.GetAxis("Mouse ScrollWheel");
        float newView = currCamera.fieldOfView - Wheel * WheelSpeed;
        newView = Mathf.Clamp(newView, 20f, 70f);

        //新的Field of View

        //使用Lerp插值让相机位置平滑移动
        //float Mathf.Lerp
        //Vector3 Vector3.Lerp
        //Quatenion quatenion.Lerp
        transform.position = Vector3.Lerp(transform.position, newPos, moveSpeed * Time.deltaTime);
        currCamera.fieldOfView = Mathf.Lerp(currCamera.fieldOfView, newView, moveSpeed * Time.deltaTime);
    }
}
