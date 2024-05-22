using System;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

class PlayerMovement : MonoBehaviour
{
    float speed;
    float currentSpeed;
    float faceX = 0f;
    float faceY = -1f;
    float timeCheck = 0f;
    float timeCheckSwing = 0f;


    Rigidbody2D rb;
    GameObject toolChild;
    PlayerLeftClick pLClick;
    PlayerController pController;
    BoxCollider2D chargedHitBox;
    public Vector2 nowFacing;
    Rotate nowRotation;

    public PlayerMovement()
    {

    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        toolChild = transform.GetChild(0).gameObject;
        pLClick = this.gameObject.GetComponent<PlayerLeftClick>();
        pController = this.gameObject.GetComponent<PlayerController>();
        chargedHitBox = this.gameObject.GetComponentInChildren<BoxCollider2D>();
        currentSpeed = 5f;
    }
    private void Update()
    {
        PlayerSpeed(); // 이동속도 제어기
        float x = Input.GetAxisRaw("Horizontal"); ;
        float y = Input.GetAxisRaw("Vertical"); ;
        
        if (pLClick.Fishing) //낚싯대 차징
        {
            x = 0; y = 0;
            NormalMovement(x, y);
            ChargeHitBox();
            double xx = chargedHitBox.transform.position.x;
            double yy = chargedHitBox.transform.position.y;
            if (math.round(xx) > xx) { xx = math.round(xx) - 0.5; } else { xx = math.round(xx) + 0.5; }
            if (math.round(yy) > yy) { yy = math.round(yy) - 0.5; } else { yy = math.round(yy) + 0.5; }
            chargedHitBox.transform.position = new Vector2((float)xx + faceX,(float)yy + faceY);

        }
        else if (pLClick.toolUsed) //도구모션
        {
            x = 0; y = 0;
            NormalMovement(x, y);
        }
        else if (pLClick.chargeTool) //도구 차징
        {
            ChargeMovement(); // 차징 동작을 하는 상태  물뿌리개, 괭이
            ChargeHitBox(); // 차징중 히트 박스의 위치
        }
        else
        {
            NormalMovement(x, y);
            NormalChargeHitBox();
        }

        if (pLClick.toolSwing)
        {
            SwingTool();
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

    void NormalMovement(float x, float y)
    {
        rb.velocity = new Vector2(x * currentSpeed, y * currentSpeed);
        Facing(x, y);
        nowFacing = new Vector2(faceX, faceY);

        if (Mathf.Abs(faceX) > 0) { toolChild.transform.rotation = Quaternion.Euler(0, 0, faceX * 90); }
        else if (Mathf.Abs(faceY) > 0) { toolChild.transform.rotation = Quaternion.Euler(0, 0, 270 - 90 * faceY); }
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
                Vector3 temp = new Vector2(x, y).normalized;
                this.transform.position += temp * 0.5f;
                timeCheck = 0;
            }
        }
        else { timeCheck = 0; }
    }

    void NormalChargeHitBox()
    {
        double xx = math.round(this.transform.position.x);
        if (xx > this.transform.position.x) { xx -= 0.5; } else { xx += 0.5; }
        double yy = math.round(this.transform.position.y);
        if (yy > this.transform.position.y) { yy -= 0.5; } else { yy += 0.5; }
        chargedHitBox.transform.position = new Vector2((float)xx, (float)yy) + nowFacing;
        chargedHitBox.transform.localScale = new Vector2(0.8f,0.8f);
        if (nowFacing.x == 1 || nowFacing.x == -1)
        { chargedHitBox.transform.rotation = Quaternion.Euler(0, 0, 0); }
        else if (nowFacing.y == 1 || nowFacing.y == -1)
        { chargedHitBox.transform.rotation = Quaternion.Euler(0, 0, 90); }
    }
    void ChargeHitBox()
    {
        chargedHitBox.transform.localScale = Vector2.one;

        double xx = math.round(this.transform.position.x);
        if (xx > this.transform.position.x) { xx -= 0.5; } else { xx += 0.5; }
        double yy = math.round(this.transform.position.y);
        if (yy > this.transform.position.y) { yy -= 0.5; } else { yy += 0.5; }
        chargedHitBox.transform.position = new Vector2((float)xx, (float)yy) + nowFacing * pLClick.chargeLevel;
        chargedHitBox.transform.localScale = pLClick.colSize;
        if (nowFacing.x ==1 || nowFacing.x == -1)
        {chargedHitBox.transform.rotation = Quaternion.Euler(0, 0, 0);}
        else if (nowFacing.y == 1 ||  nowFacing.y == -1) 
        {chargedHitBox.transform.rotation = Quaternion.Euler(0, 0, 90);}
    }

    void PlayerSpeed() // 이동속도제어 : 탈진,기본,커피,말 등등
    {
        if (pController.currentStamina > 0) { speed = 5f; }
        else if (pLClick.toolUsed) { speed = 0f; }
        else { speed = 1f; }

        currentSpeed = speed;
    }

    void SwingTool()
    {
        timeCheck += Time.deltaTime;
        if (MathF.Abs(faceX) == 1) // 좌우를 보는 상태라면
        {
            toolChild.transform.rotation = Quaternion.Euler(0, 0, 0 - (timeCheck / 0.5f) * 180 * (-faceX));
        }
        else
        {
            if (faceY == 1)
            {
                toolChild.transform.rotation = Quaternion.Euler(0, 0, -90 - (timeCheck / 0.5f) * 180);
            }
            else if (faceY == -1)
            {
                toolChild.transform.rotation = Quaternion.Euler(0, 0, 90 - (timeCheck / 0.5f) * 180);
            }
        }


        this.gameObject.GetComponentInChildren<EdgeCollider2D>().enabled = true;
        if (timeCheck > 0.5f)
        {
            timeCheck = 0f;
        }


    }

}