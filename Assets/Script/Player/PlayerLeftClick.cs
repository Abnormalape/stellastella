using System;
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
    public float coolDownTime = 0.5f;
    float passedTime = 0f;
    float chargeTime = 0f;
    float throwPower = 0f;
    public bool toolUsed;
    public bool toolSwing;
    bool onCharge = false;

    private void Awake()
    {
        pCon = this.gameObject.GetComponent<PlayerController>();
        pMov = this.gameObject.GetComponent<PlayerMovement>();
        pInven = this.gameObject.GetComponent<PlayerInventroy>();

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
                }
            }
        }

        // 도구를 사용한 시점부터 시간 체크
        if (toolUsed) { passedTime += Time.deltaTime; }
        CoolDown();

        if (onCharge && Input.GetMouseButtonUp(0)) // 차지중인 상태에서 좌클릭을 뗀다면
        {   //낚싯대 이거나, 다른 도구이거나

            if (currentData.toolType == 9) // 낚싯대라면
            {
                MakeThrowColliderAct(); // 던지는 콜라이더 생성
            }
            else    // 그 외라면
            {
                MakeChargeColliderAct(); // 범위 콜라이더 생성
            }
            onCharge = false;
        }

        if (this.gameObject.GetComponentInChildren<EdgeCollider2D>().enabled == true && !toolSwing) // 엣지가 켜졌는데 휘두르는게 아니면 = 콜라이더가 즉시 생기지 않는 부류
        { Invoke("ColliderOff", 0.1f); }        // 곧바로 꺼라
        else if (toolSwing)                     // 콜라이더가 즉시 생기는 부류
        { Debug.Log("ASD"); Invoke("ColliderOff", coolDownTime);  // 쿨타임이 지난후 꺼라, 
            toolSwing = false;}                 // 휘두르기도 꺼라
    }
    private void LateUpdate()
    {

    }
    void UseAxe() // 완료
    {
        Invoke("MakeCollider", coolDownTime);
        StopPlayer();
        StaminaUse();
    }
    //n초후 바라보는 방향에 콜라이더 생성 후 제거, 휘두르는 동안 다른 움직임 불가, 스테미너 소모
    void UseHoe() // 미완 : boxcollider
    {
        if (currentData.grade == 1)
        {
            StopPlayer();
            StaminaUse();
            Invoke("MakeCollider", coolDownTime);
        }
        else if (currentData.grade > 1)
        {
            MakeChargeColliderCharge();
        }
    }
    //charge후 내 위치를 기준으로 전방에 다수의 콜라이더 생성 후 제거,n초후 바라보는 방향에 콜라이더 생성 후 제거,
    //차징중 움직임 변경, 사용중 다른 움직임 불가, 스테미너 소모
    void UseWaterCan() // 미완 : boxcollider
    {
        if (currentData.grade == 1)
        {
            StopPlayer();
            StaminaUse();
            Invoke("MakeCollider", coolDownTime);
        }
        else if (currentData.grade > 1)
        {
            MakeChargeColliderCharge();
        }
    }
    //charge후 내 위치를 기준으로 전방에 다수의 콜라이더 생성 후 제거,n초후 바라보는 방향에 콜라이더 생성 후 제거,
    //차징중 움직임 변경, 사용중 다른 움직임 불가, 스테미너 소모
    //물 채우기 가능
    void UsePickAxe() // 완료
    {
        Invoke("MakeCollider", coolDownTime);
        StopPlayer();
        StaminaUse();
    }
    //n초동안 움직이는 콜라이더 생성 이후 제거, 휘두르는 동안 다른 움직임 불가, 스테미너 소모
    void UseSickle()
    {
        StopPlayer();
        MakeMovingColliderSickle();
    }
    //n초후 바라보는 방향에 콜라이더 생성 후 제거, 휘두르는 동안 다른 움직임 불가
    void UseFishingRod()
    {
        MakeThrowColliderCharge();
    }
    //charge후 전방에 콜라이더 생성, 

    void StaminaUse()// 플레이어 컨트롤에 체력 스테미너 놓기
    {
        pCon.currentStamina -= currentData.staminaRestor; // 플레이어 컨트롤의 현재 스테미너가 현재아이템데이터의 스테미나 회복량만큼 감소
    }

    void StopPlayer() { toolUsed = true; } // toolUsed가 true일때 도구사용불가, 이동불가, 등등

    void MakeCollider() //엣지콜라이더 킴, 위치, 회전은 movement에서
    { this.gameObject.GetComponentInChildren<EdgeCollider2D>().enabled = true; }

    void MakeChargeColliderCharge() //범위용 박스 콜라이더를 생성, 필요시 크기 위치 변경
    { // 등급이 2이상이면 클릭하거나 누르고 있을때, 차징 시간이 계산되며, 뗐을때 조건에 맞는 크기의 콜라이더가 생성된다.
        if (Input.GetMouseButton(0)) // 누르고 있는동안 아래를 실행
        {
            this.gameObject.GetComponent<PlayerMovement>().chargeMove = true;// 움직임의 차징상태를 true로 변경
            chargeTime += Time.deltaTime;
            onCharge = true; // 차징 종료시 동작을 위한 bool
        }

    } // 범위용 박스콜라이더

    void MakeChargeColliderAct()
    {
        if (chargeTime < 1f)
        {
            MakeCollider();
        }
        else if (chargeTime >= 1f && currentData.grade >= 2) //1단계 차지, 등급 2 이상시 3칸
        {
            //this.gameObject.GetComponentInChildren<BoxCollider2D>().enabled = true; // 잠깐 콜라이더를 킨다. 근데 지금은 일단 꺼두자.
            //콜라이더 크기 조정 -> getchildcomponent<boxcollider>.size
            //콜라이더 위치 조정 -> transform.position
        }
        else if (chargeTime >= 2f && currentData.grade >= 3) //2단계 차지 5칸
        {

        }
        else if (chargeTime >= 3f && currentData.grade >= 4) //3단계 차지 9칸
        {

        }
        else if (chargeTime >= 4f && currentData.grade >= 5) //4단계 차지 18칸
        {

        }
        StaminaUse();
        StopPlayer();
        chargeTime = 0f;
        this.gameObject.GetComponent<PlayerMovement>().chargeMove = false;
    }

    void MakeThrowColliderCharge()  //낚싯대일때는 무조건 차징, 다른 차징도구와 다르게 움직이는것 불가
                                    //차징파워만 계산
    {
        pMov.chargeFishing = true; // 낚싯대 차징중
        chargeTime += Time.deltaTime; // 1초? 2초? 일때 풀 차징
        if (chargeTime > 2f) { chargeTime = 0f; } //2초 이상에서 초기화
        else if (chargeTime < 1f) //1초까지 증가
        {throwPower = chargeTime;}
        else { throwPower = 2f - chargeTime; } //1~2초 감소
        
        onCharge = true; // 차징종료시 행동을 위한 bool
    }

    void MakeThrowColliderAct() //낚싯대사용, 차징파워에 따라 찌 생성위치 변경
    {
        Debug.Log(throwPower);
        Debug.Log("Throw bobble");
        pMov.chargeFishing = false; // 낚싯대 차징중 아님
        StaminaUse();
        StopPlayer();
        throwPower = 0f;
        chargeTime = 0f;
    }
    void MakeMovingColliderSickle() //낫
    {
        this.gameObject.GetComponentInChildren<EdgeCollider2D>().enabled = true;
        toolSwing = true;
    }
    void MakeMovingColliderWeapon() //무기
    {
        this.gameObject.GetComponentInChildren<EdgeCollider2D>().enabled = true;
    }

    void WaterLevel() //물뿌리개
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
}
