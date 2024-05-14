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
    float coolDownTime = 0.5f;
    float passedTime = 0f;
    float chargeTime = 0f;
    float throwPower = 0f;
    public bool toolUsed;
    bool onCharge = false;
    private void Awake()
    {
        pCon = this.gameObject.GetComponent<PlayerController>();
        pMov = this.gameObject.GetComponent<PlayerMovement>();
        pInven = this.gameObject.GetComponent<PlayerInventroy>();
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

        if (toolUsed)
        {
            passedTime += Time.deltaTime;
        }
        CoolDown();

        if (onCharge && Input.GetMouseButtonUp(0))
        {
            MakeChargeColliderAct();
        }
    }
    // 키를 떼었을때 동작 => 괭이 물뿌리개 낚시대
    // 키를 누르는 동안은 차징
    // 키를 눌렀을때 동작

    void UseAxe() // 완료
    {
        MakeCollider();
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
            MakeCollider();
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
            MakeCollider();
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
        MakeCollider();
        StopPlayer();
        StaminaUse();
    }
    //n초동안 움직이는 콜라이더 생성 이후 제거, 휘두르는 동안 다른 움직임 불가, 스테미너 소모
    void UseSickle()
    {
        StopPlayer();
    }
    //n초후 바라보는 방향에 콜라이더 생성 후 제거, 휘두르는 동안 다른 움직임 불가
    void UseFishingRod()
    {

    }
    //charge후 전방에 콜라이더 생성, 

    void StaminaUse() { } // 플레이어 컨트롤에 체력 스테미너 놓기

    void StopPlayer() { toolUsed = true; } // toolUsed가 true일때 도구사용불가, 이동불가, 등등

    void MakeCollider() //엣지콜라이더 킴, 위치, 회전은 movement에서
    { this.gameObject.GetComponentInChildren<EdgeCollider2D>().enabled = true; }

    void MakeChargeColliderCharge() //범위용 박스 콜라이더를 생성, 필요시 크기 위치 변경
    { // 등급이 2이상이면 클릭하거나 누르고 있을때, 차징 시간이 계산되며, 뗐을때 조건에 맞는 크기의 콜라이더가 생성된다.
        if (Input.GetMouseButton(0)) // 누르고 있는동안 아래를 실행
        {
            this.gameObject.GetComponent<PlayerMovement>().chargeMove = true;// 움직임의 차징상태를 true로 변경
            chargeTime += Time.deltaTime;
            onCharge = true;
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
        //this.gameObject.GetComponentInChildren<BoxCollider2D>().enabled = true; // 잠깐 콜라이더를 킨다. 근데 지금은 일단 꺼두자.
    }

    void MakeThrowColliderCharge()
    {

    }

    void MakeThrowColliderAct()
    {

    }
    void MakeMovingCollider() // 이거 판정을 모르겠다
    {
        this.gameObject.GetComponentInChildren<EdgeCollider2D>().enabled = true;
    }

    void WaterLevel()
    {

    }

    void CoolDown()
    {
        if (passedTime > coolDownTime)
        {
            toolUsed = false;
            passedTime = 0;
        }
    }
}
