using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed = 5f;
    float faceX;
    float faceY;
    float toolLag; // 도구별로 쿨타임이 달라서 외부에서 받아와야 하나 임시로 여기에 둔다
    bool doToolLag; // 현재 쿨타임인지 판정하는 거라 여기다 둬도 된다.
    bool touchObject;
    Rigidbody2D rb;
    public GameObject facing;
    [SerializeField]
    LayerMask objectLayer;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        faceX = 0;
        faceY = -1;
        toolLag = 0f;
        doToolLag = false;
        touchObject = false;
    }

    
    void Update()
    {
        touchObject = Physics2D.Linecast(this.transform.position, new Vector2(this.transform.position.x + faceX * 0.7f, this.transform.position.y + faceY * 0.7f), objectLayer);
        Walk();   
        float x;
        float y;
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        if (!doToolLag){rb.velocity = new Vector3(x * speed, y * speed, y * speed);}
        else if (doToolLag) { rb.velocity = Vector3.zero; }
        this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.y);
        UseTool();
        Facing(x,y);
        if (!doToolLag) { facing.transform.position = new Vector2(this.transform.position.x + faceX * 0.7f, this.transform.position.y + faceY * 0.7f); }
        else if (doToolLag) { facing.transform.position = facing.transform.position; }
    }

    void Walk()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = 3f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 5f;
        }
    }
    
    void UseTool()  // 좌클릭에 대한 반응 -> 좌클릭과 좌클릭 사이에는 텀이 있어야 한다. && 좌클릭했다면 움직임이 멈추어야 한다.
                    // 각 도구에 따라 좌클릭 모션이 다르다(물뿌리개, 낚싯대). 씨앗의 경우는 좌클릭하면 모션없이 씨앗을 심는다. 무기는 텀이 무기마다 다르다. 
                    // 손에 든 아이템을 판정하고 그 결과에 따라 DB등에서 모션 등을 불러온다.
    {
        if(Input.GetMouseButtonDown(0) && doToolLag == false && touchObject) //오브젝트 레이어 물체와 접촉한 상태에서 좌클릭을 한다면
        {
            //접촉한 물체의 상태를 변화 시킨다
            doToolLag = true;
        }
        else if (Input.GetMouseButtonDown(0) && doToolLag == false) // 단순히 클릭만 한 경우 + 도구 쿨타임이 돈 경우
        {
            doToolLag = true;
        }

        if (doToolLag) // 만약 클릭을 했다.
        {
            toolLag += Time.deltaTime;
            if(toolLag > 0.9f) { GetComponentInChildren<EdgeCollider2D>().enabled = true; }
            if (toolLag > 1f) // 만약 클릭을 한지 1초가 지났다면
            {
                
                doToolLag = false; // 다시 클릭 할수 있게 하고
                toolLag = 0f; // 쿨타임을 초기화한다.
            }
        }
        if (toolLag == 0f) { GetComponentInChildren<EdgeCollider2D>().enabled = false;}
    }
    
    void Facing(float x, float y) // 현재바라보는 방향 = 상호작용방향,스프라이트방향
    {
        if (MathF.Abs(x) > 0)
        {
            faceX = x;
            faceY = 0;
        }
        else if (MathF.Abs(y) > 0)
        {
            faceX = 0;
            faceY = y;
        }
    }
}
