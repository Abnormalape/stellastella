using System;
using System.Threading;
using Unity;
using Unity.VisualScripting;
using UnityEngine;

class PlayerLeftClick : MonoBehaviour
// 얘를 플레이어 컨트롤에 넣고, 툴타입을 받아온다
{                       // 툴 타입에 맞는 메서드를 실행한다
                        // 그냥 얘 자체를 update에 실행해도 되는것 아닌가?

    PlayerController pCon;
    PlayerMovement pMov;
    PlayerInventroy pInven;
    ItemDB currentData;
    GameObject chargedHitBox;
    public Vector2 colSize;
    public float chargeLevel;
    public float coolDownTime = 0.5f;
    float passedTime = 0f;
    float chargeTime = 0f;
    float throwPower = 0f;
    public bool toolUsed;
    public bool toolSwing;
    public bool chargeTool;
    public bool chargeFishing;
    bool checkMouse;

    private void Awake()
    {
        pCon = this.gameObject.GetComponent<PlayerController>();
        pMov = this.gameObject.GetComponent<PlayerMovement>();
        pInven = this.gameObject.GetComponent<PlayerInventroy>();
        chargedHitBox = this.gameObject.GetComponentInChildren<BoxCollider2D>().transform.gameObject; // 내 게임 오브젝트의 자식중 박스콜라이더를 찾아서 그놈의 게임오브젝트를 반환
        chargedHitBox.GetComponent<BoxCollider2D>().enabled = false;
        this.gameObject.GetComponentInChildren<EdgeCollider2D>().enabled = false;
    }
    private void Update()
    {
        
        currentData = new ItemDB(pInven.currentInventoryItem);

        if (!toolUsed)
        {
            if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
            {
                switch (currentData.toolType)
                {
                    case 1:
                        UseAxe();
                        return;
                    case 2:
                        UseHoe();
                        return;
                    case 3:
                        UseWaterCan();
                        return;
                    case 4:
                        UsePickAxe();
                        return;
                    case 5:
                        UseSickle();
                        return;
                    case 9:
                        UseFishingRod();
                        return;
                    case 0: //임시
                        UseAxe();
                        return;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            switch (currentData.toolType)
            {
                case 2:
                    UseHoe();
                    return;
                case 3:
                    UseWaterCan();
                    return;
                case 9:
                    UseFishingRod();
                    return;
            }
        }
        
        if(toolSwing == true) { passedTime += Time.deltaTime;
        if(passedTime > 0.5f) { ColliderSwingOff(); }
        }
    }
    private void LateUpdate()
    {

    }
    void UseAxe() // 완료
    {
        DoMotion();
        StaminaUse();
        Invoke("MakeCollider", 0.5f); // 0.5f는 기본적으로 정해진 도구 사용 시간이다.
    }

    void UsePickAxe() // 완료
    {
        DoMotion();
        StaminaUse();
        Invoke("MakeCollider", 0.5f);
    }

    void UseHoe() 
    {
        if(currentData.grade == 1) // 1단계 일때 도끼와 같이 행동
        {
            DoMotion();
            StaminaUse();
            Invoke("MakeCollider", 0.5f);
        }
        else // 그 외엔 차징 후, 모션 후 콜라이더 생성
        {
            MakeChargeColliderCharge();
        }
    }
    
    void UseWaterCan() 
    {
        if (currentData.grade == 1)
        {
            DoMotion();
            StaminaUse();
            Invoke("MakeCollider", 0.5f);
        }
        else
        {
            MakeChargeColliderCharge();
        }
    }
    
    void UseSickle()
    {
        DoMotion();
        MakeMovingColliderSwing();
    }
    
    void UseFishingRod()
    {
        MakeThrowColliderCharge();
    }
    

    void StaminaUse()// 플레이어 컨트롤에 체력 스테미너 놓기
    {pCon.currentStamina -= currentData.staminaRestor;}

    
    void DoMotion() // 모션(휘두르기), 휘두르기 시간은 무기 제외 0.5초로 고정
    {toolUsed = true;}

    
    void MakeCollider() // 모션 '후' 콜라이더 생성, 도구 사용 종료
    {   this.gameObject.GetComponentInChildren<EdgeCollider2D>().enabled = true;
        toolUsed = false;
        Invoke("ColliderOff", 0.1f);
    }

    void MakeChargeColliderCharge() // 차징 - 괭이 물뿌리개
    { // 등급이 2이상이면 클릭하거나 누르고 있을때, 차징 시간이 계산되며, 뗐을때 조건에 맞는 크기의 콜라이더가 생성된다.
        if (Input.GetMouseButton(0)) // 누르고 있는동안 아래를 실행
        {
            chargeTool = true;// 움직임의 차징상태를 true로 변경

            chargeTime += Time.deltaTime;
            if (chargeTime >= 4f && currentData.grade >= 5)
            {
                colSize = new Vector2(5.8f, 2.8f); ;
                chargeLevel = 3.5f;
            }
            else if (chargeTime >= 3f && currentData.grade >= 4)
            {
                colSize = new Vector2(2.8f, 2.8f);
                chargeLevel = 2f;
            }
            else if (chargeTime >= 2f && currentData.grade >= 3)
            {
                colSize = new Vector2(4.8f, 0.8f);
                chargeLevel = 3f;
            }
            else if (chargeTime >= 1f && currentData.grade >= 2)
            {
                colSize = new Vector2(2.8f, 0.8f);
                chargeLevel = 2f;
            }
        }
        if (Input.GetMouseButtonUp(0)) // 마우스를 떼었을때 Act
        {
            StaminaUse();
            DoMotion();
            if (chargeTime < 1f) // 차징이 1초 미만이라면 일반 사용과 같고
            {
                StaminaUse();
                chargeTime = 0f;
                Invoke("MakeCollider", 0.5f);
            }
            else
            {
                Invoke("MakeChargeColliderAct", 0.5f);
            }
            chargeTool = false;
        }
    }

    void MakeChargeColliderAct() // 차징 후, 모션 후 콜라이더 생성 - 괭이 물뿌리개
    {
        chargedHitBox.GetComponent<BoxCollider2D>().enabled = true;
        chargeTime = 0f;
        toolUsed = false;
        Invoke("ColliderOff", 0.1f);
        Invoke("BoxColliderOff", 0.1f);
    }

    void MakeThrowColliderCharge()  // 차징 - 낚싯대
    {
        chargeFishing = true; // 낚싯대 차징중
        if (Input.GetMouseButton(0))
        {
            chargeTime += Time.deltaTime; // 1초일때 풀 차징
            if (chargeTime > 2f) { chargeTime = 0f; } //2초 이상에서 초기화
            else if (chargeTime < 1f) //1초까지 증가
            { throwPower = chargeTime; }
            else { throwPower = 2f - chargeTime; } //1~2초 감소
        }
        if (Input.GetMouseButtonUp(0))
        {
            DoMotion();
            Invoke("MakeThrowColliderAct", 0.5f); // throwpower에 비례해 던지는 시간 길어짐
            chargeFishing = false;
        }
        
    }

    void MakeThrowColliderAct() // 차징 후, 모션 후 콜라이더 생성 - 낚싯대
    {
        // throwpower에 맞는 거리에 collider 생성
        
        StaminaUse();
        throwPower = 0f;
        chargeTime = 0f;
        chargeFishing = false; // 낚싯대 차징중 아님
        toolUsed = false;
        Invoke("ColliderOff", 0.1f);
    }
    void MakeMovingColliderSwing() // 모션 중 콜라이더 생성
    {
        toolSwing = true;
    }

    void ColliderSwingOff()
    {
        toolUsed = false;
        toolSwing = false;
        passedTime = 0f;
        ColliderOff();
    }
    

    void WaterLevel() // 물뿌리개
    {
        // 얘가 자기 앞의 오브젝트? 레이어? 아무튼 판정해서 물이면 나의 물 레벨을 채운다... 인데
    }

    void CoolDown()
    {
        if (passedTime > coolDownTime)
        {
            toolUsed = false;
            passedTime = 0;
        }
    }
    void ColliderOff() { this.gameObject.GetComponentInChildren<EdgeCollider2D>().enabled = false; }
    void BoxColliderOff() { this.gameObject.GetComponentInChildren<BoxCollider2D>().enabled = false; }
}
