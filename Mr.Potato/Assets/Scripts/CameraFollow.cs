using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player;  //主角的Transform   设置成public就要在Unity里把hero拖进去赋初值或在下面写Awake函数，private就必须在下面写一个Awake函数
    private float xMargin = 3.0f;  //x方向超过多少就移动摄像机
    private float yMargin = 1.0f;  //y方向超过多少就移动摄像机
    private float SmoothX = 1.0f;  //摄像机移动的速度
    private float SmoothY = 1.0f;
    private float MaxCamX = 5.0f;  //摄像机移动的范围
    private float MaxCamY = 4.0f;
    private float MinCamX = -5.0f;
    private float MinCamY = -1.0f;
    /*  //方法2  注意下方Clamp函数改成CamX.x/y 和CamY.x/y
    private Vector2 CamX = new Vector2(-5, 5);
    private Vector2 CamY = new Vector2(-1, 4);
    */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //player = GameObject.Find("Hero").transform;  //方法2
    }

    bool NeedMoveX()
    {
        return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
        /*
        bool bMove = false;
        */
         
    }

    bool NeedMoveY()
    {
        return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
    }

    private void FixedUpdate()
    {
        TrackPlayer();
    }

    void TrackPlayer()
    {
        float CamNewX = transform.position.x;
        float CamNewY = transform.position.y;

        if(NeedMoveX())  //计算新摄像机位置
            CamNewX = Mathf.Lerp(transform.position.x, player.position.x, SmoothX * Time.deltaTime);
            
        if(NeedMoveY())
            CamNewY = Mathf.Lerp(transform.position.y, player.position.y, SmoothY * Time.deltaTime);

        CamNewX = Mathf.Clamp(CamNewX, MinCamX, MaxCamX);  //将新相机固定在合法位置内
        CamNewY = Mathf.Clamp(CamNewY, MinCamY, MaxCamY);

        transform.position = new Vector3(CamNewX, CamNewY, transform.position.z);
    }
}
