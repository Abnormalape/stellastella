using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

class PlayerMovement : MonoBehaviour
{
    float speed = 5f;
    float faceX = 0f;
    float faceY = -1f;
    float timeCheck = 0f;
    public bool chargeMove = false;

    Rigidbody2D rb;
    GameObject toolChild;
    PlayerLeftClick pLClick;
    Vector2 nowFacing;
    
    
    public PlayerMovement()
    {

    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        toolChild = transform.GetChild(0).gameObject;
        pLClick = this.gameObject.GetComponent<PlayerLeftClick>();
    }
    private void Update()
    {
        if(chargeMove && pLClick.toolUsed == false)
        {
            ChargeMovement();
        }
        else if (pLClick.toolUsed == false)
        {
            float x;
            float y;
            x = Input.GetAxisRaw("Horizontal");
            y = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(x * speed, y * speed);
            Facing(x, y);
            nowFacing = new Vector2(faceX, faceY);
            toolChild.transform.position = new Vector2(faceX + this.transform.position.x, faceY + this.transform.position.y);
            if (Mathf.Abs(faceX) > 0) { toolChild.transform.rotation = Quaternion.Euler(0, 0, 0f); }
            else if (Mathf.Abs(faceY) > 0) { toolChild.transform.rotation = Quaternion.Euler(0, 0, 90); }
        }
        else if (pLClick.toolUsed == true)
        {
            rb.velocity = Vector2.zero;
        }

        if (!chargeMove && timeCheck!=0)
        {
            timeCheck = 0;
        }
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

    void ChargeMovement()   // 도구 차징중에 이동하는 방법, 바라보는 방향은 그대로 이고, 키가 입력된 방향으로 일정주기마다 점프
    {                       // 어차피 바라보는 방향 안 건드리면 안바뀌니, 포지션만 바꾸면 될듯
        rb.velocity = Vector3.zero;

        float x;
        float y;
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        
        if (Mathf.Abs(x) > 0 || Mathf.Abs(y) > 0) // 키입력이 있는 동안엔
        {
            timeCheck += Time.deltaTime; // 시간이 흘러가고
            if (timeCheck > 0.5f) // 누른지 0.3초가 지나면
            { // 누른 방향으로 이동한다
                Vector3 temp = new Vector2 (x, y).normalized;
                this.transform.position += temp * 0.5f;
                timeCheck = 0;
            }
        }
    }


}