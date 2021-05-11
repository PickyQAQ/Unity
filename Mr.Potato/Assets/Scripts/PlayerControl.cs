using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update


    private Rigidbody2D heroBody;
    public float moveForce = 100;
    public float jumpForce = 100;
    private float fInput = 0.0f;
    public float maxSpeed = 5;
    private bool bFaceRight = true;
    private bool bGrounded = false;
    Transform mGroundCheck;

    




    void Start()
    {
        heroBody = GetComponent<Rigidbody2D>();
        mGroundCheck = transform.Find("GroundCheck");  //判断主角是否在Ground上
    }


    // Update is called once per frame  !!!!!
    void Update()
    {
        fInput = Input.GetAxis("Horizontal");
        if (fInput < 0 && bFaceRight)  //判断是否满足转身条件
            flip();

        else if (fInput > 0 && !bFaceRight)  //判断是否满足转身条件
            flip();

        bGrounded = Physics2D.Linecast(transform.position, mGroundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

    }


    private void FixedUpdate()
    {
        if (Mathf.Abs(heroBody.velocity.x) < maxSpeed)  //速度小于最大速度时加力
            heroBody.AddForce(fInput * moveForce * Vector2.right);

        if (Mathf.Abs(heroBody.velocity.x) > maxSpeed)  //速度大于最大速度时，将速度控制在最大速度
            heroBody.velocity = new Vector2(Mathf.Sign(heroBody.velocity.x) * maxSpeed, heroBody.velocity.y);

        bool bJump = false;

        //if (Input.GetButtonDown("Jump") && bGrounded)  //检测到跳跃键和主角在Ground上时，起跳
        if (bGrounded)
            bJump = Input.GetButtonDown("Jump"); 
            if(bJump)  //如果起跳，则向上加力
                heroBody.AddForce(new Vector2(0f, jumpForce));

    }


    void flip()  //转身
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        bFaceRight = !bFaceRight;
    }

}
