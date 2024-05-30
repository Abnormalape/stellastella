using System;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

class PlayerMovement : MonoBehaviour // swingtool 미완성
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
    PlayerController pCon;
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
        pCon = this.gameObject.GetComponent<PlayerController>();
        chargedHitBox = this.gameObject.GetComponentInChildren<BoxCollider2D>();
        currentSpeed = 5f;
    }
    private void Update()
    {
        PlayerSpeed(); // 이동속도 제어기
        float x = Input.GetAxisRaw("Horizontal"); ;
        float y = Input.GetAxisRaw("Vertical"); ;

        //x = 0; y = 0;
        //NormalMovement(x, y);
        //ChargeHitBox();
        //double xx = chargedHitBox.transform.position.x;
        //double yy = chargedHitBox.transform.position.y;
        //if (math.round(xx) > xx) { xx = math.round(xx) - 0.5; } else { xx = math.round(xx) + 0.5; }
        //if (math.round(yy) > yy) { yy = math.round(yy) - 0.5; } else { yy = math.round(yy) + 0.5; }
        //chargedHitBox.transform.position = new Vector2((float)xx + faceX, (float)yy + faceY);

        //상태종류 : 대기, 움직임, 차징, 낚시 차징, 입질대기, 모션, 대화, 낚시 미니게임

        //moving
        if (pCon.charge) //차징상태에선.
        {   //차지 이동만 가능하다.
            ChargeMovement();
        }
        else if (pCon.idle || pCon.moving) // 정지해 있거나 움직이는 중이라면
        {   //움직일 수 있다.
            NormalMovement(x, y);
        }
        else if (pCon.fishCharge || pCon.motion || pCon.minigame || pCon.waitingForBait || pCon.conversation || pCon.inventory) // 이 상태에선.
        {   //움직일수 없다.
            StopMovement();
        }
        else
        {
            StopMovement();
        }


        //facing
        if (pCon.idle || pCon.moving) // 움직이거나 멈춰있는 상황에선.
        {   //얼굴을 돌릴수 있다.
            Facing(x, y);
        }
        else // 그 외엔
        {   //얼굴을 돌릴수 없다.

        }

        //히트박스
        if (pCon.charge) // 차징중일때 차지 히트박스 실행
        {
            ChargeHitBox();
        }
        else if (pCon.fishCharge) // 낚시 차징중 일때 낚시 히트박스 실행
        {
            ChargeHitBox();
        }
        else // 그 외엔 전부 통상 히트박스
        {
            NormalChargeHitBox();
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

    void StopMovement()
    {
        rb.velocity = Vector2.zero;
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
        chargedHitBox.transform.localScale = new Vector2(0.8f, 0.8f);
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
        if (nowFacing.x == 1 || nowFacing.x == -1)
        { chargedHitBox.transform.rotation = Quaternion.Euler(0, 0, 0); }
        else if (nowFacing.y == 1 || nowFacing.y == -1)
        { chargedHitBox.transform.rotation = Quaternion.Euler(0, 0, 90); }
    }

    void PlayerSpeed() // 이동속도제어 : 탈진,기본,커피,말 등등
    {
        if (pCon.currentStamina > 0) { speed = 5f; }
        else { speed = 1f; }

        currentSpeed = speed;
    }
}