using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

class PlayerMovement : MonoBehaviour
{
    float speed;
    float currentSpeed;
    float faceX = 0f;
    float faceY = -1f;
    float timeCheck = 0f;
    float timeCheckSwing = 0f;
    public bool chargeMove = false;
    public bool chargeFishing = false;

    Rigidbody2D rb;
    GameObject toolChild;
    PlayerLeftClick pLClick;
    PlayerController pController;
    Vector2 nowFacing;
    
    
    public PlayerMovement()
    {

    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        toolChild = transform.GetChild(0).gameObject;
        pLClick = this.gameObject.GetComponent<PlayerLeftClick>();
        pController = this.gameObject.GetComponent<PlayerController>();
        currentSpeed = 5f;
    }
    private void Update()
    {
        PlayerSpeed(); // 이동속도 제어기


        if(chargeMove && pLClick.toolUsed == false) // 차징중이며, 도구사용 모션이 들어가지 않았을때
        {ChargeMovement();}
        else if (pLClick.toolUsed == true || chargeFishing) // 도구사용 모션에 들어갔거나, 낚싯대를 차징중일때
        { rb.velocity = Vector2.zero; }
        else if (pLClick.toolSwing) // 휘두르기 상태라면
        {SwingTool();}
        else if (pLClick.toolUsed == false) // 일반상태
        {
            float x;
            float y;
            x = Input.GetAxisRaw("Horizontal");
            y = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(x * currentSpeed, y * currentSpeed);
            Facing(x, y);
            nowFacing = new Vector2(faceX, faceY);
            
            if (Mathf.Abs(faceX) > 0) { toolChild.transform.rotation = Quaternion.Euler(0, 0, faceX * 90); }
            else if (Mathf.Abs(faceY) > 0) { toolChild.transform.rotation = Quaternion.Euler(0, 0, 270 - 90 * faceY); }
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

    void PlayerSpeed() // 이동속도제어 : 탈진,기본,커피,말 등등
    {
        if (pController.currentStamina > 0) {speed = 5f;}
        else {speed = 1f;}
        
        currentSpeed = speed;
    }

    void SwingTool()
    {
        rb.velocity = Vector2.zero;
        timeCheckSwing += Time.deltaTime;
        toolChild.transform.rotation = Quaternion.Euler(0, 0, this.transform.rotation.z + 90 - timeCheck / pLClick.coolDownTime * 180);
        if(timeCheck > pLClick.coolDownTime) { timeCheckSwing = 0;}
    }

}