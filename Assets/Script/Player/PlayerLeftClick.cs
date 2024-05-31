using System;
using System.Threading;
using System.Threading.Tasks;
using Unity;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

class PlayerLeftClick : MonoBehaviour // swingTool 미완성, 물뿌리개 물충전 미완성
{
    PlayerController pCon;
    PlayerMovement pMov;
    PlayerInventroy pInven;
    GameManager gameManager;
    ItemDB currentData;
    GameObject chargedHitBox;
    GameObject swingHitBox;
    public Vector2 colSize;
    public float chargeLevel;
    public float coolDownTime = 0.5f;
    float passedTime = 0f;
    float chargeTime = 0f;
    float throwPower = 0f;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        pCon = this.gameObject.GetComponent<PlayerController>();
        pMov = this.gameObject.GetComponent<PlayerMovement>();
        pInven = this.gameObject.GetComponent<PlayerInventroy>();
        chargedHitBox = this.GetComponentInChildren<BoxCollider2D>().transform.gameObject;
        chargedHitBox.GetComponent<BoxCollider2D>().enabled = false;
        swingHitBox = this.GetComponentInChildren<EdgeCollider2D>().transform.gameObject;
        swingHitBox.GetComponent<EdgeCollider2D>().enabled = false;
    }
    private void Update()
    {
        currentData = new ItemDB(pInven.currentInventoryItem);

        if (pCon.idle || pCon.moving) // 대기 상태 이거나 움직일때만 좌클릭을 누를수 있다.
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
        if (pCon.fishCharge) // 낚싯대 차징 중 일때만
        {   //낚싯대를 차징하거나 던질수 있다.
            if (Input.GetMouseButtonUp(0) || Input.GetMouseButton(0))
            {
                switch (currentData.toolType)
                {
                    case 9:
                        UseFishingRod();
                        return;
                }
            }
        }
        if (pCon.charge) // 차징 상태에서만 차징과 키떼키가 가능하다.
        {
            if (Input.GetMouseButtonUp(0) || Input.GetMouseButton(0))
            {
                switch (currentData.toolType)
                {
                    case 2:
                        UseHoe();
                        return;
                    case 3:
                        UseWaterCan();
                        return;
                }
            }
        }
        if (pCon.motion) // 모션중일땐 
        {   //쿨타임 카운트를 하고, 대기 상태로 돌아간다. 고정 쿨타임 0.5초. 
            //콜라이더는 0.3초에 생겨서 0.4초에 사라지며, 모션 종료는 0.5초이다.
            motiontime = motiontime + Time.deltaTime;
            if (motiontime > 0.5f) { motiontime = 0f; pCon.Motion(false); }
        }
        // 상기 상태 이외에는 좌클릭을 해도 효과가 없다.
    }

    float motiontime;
    void UseAxe() // 완료
    {
        //DoMotion();
        //StaminaUse();
        //Invoke("MakeBoxCollider", 0.5f);
        pCon.Motion(true);
        Invoke("StaminaUse", 0.3f); ;
        Invoke("MakeBoxCollider", 0.3f);
    }

    void UsePickAxe() // 완료
    {
        //DoMotion();
        //StaminaUse();
        //Invoke("MakeBoxCollider", 0.5f);
        pCon.Motion(true);
        Invoke("StaminaUse", 0.3f); ;
        Invoke("MakeBoxCollider", 0.3f);
    }

    void UseHoe()
    {
        if (currentData.grade == 1) // 1단계 일때 도끼와 같이 행동
        {
            //DoMotion();
            //StaminaUse();
            //Invoke("MakeBoxCollider", 0.5f);
            pCon.Motion(true);
            Invoke("StaminaUse", 0.3f); ;
            Invoke("MakeBoxCollider", 0.3f);
        }
        else // 그 외엔 차징 후, 모션 후 콜라이더 생성
        {
            pCon.Charge(true);
            MakeChargeColliderCharge();
        }
    }

    void UseWaterCan()
    {
        if (currentData.grade == 1)
        {
            //DoMotion();
            //StaminaUse();
            //Invoke("MakeBoxCollider", 0.5f);
            pCon.Motion(true);
            Invoke("StaminaUse", 0.3f); ;
            Invoke("MakeBoxCollider", 0.3f);
        }
        else
        {
            pCon.Charge(true);
            MakeChargeColliderCharge();
        }
    }

    void UseSickle()
    {
    }

    void UseFishingRod()
    {
        MakeThrowColliderCharge();
    }

    void StaminaUse()// 플레이어 컨트롤에 체력 스테미너 놓기
    { pCon.currentStamina -= currentData.staminaRestor; }
    void MakeBoxCollider()
    {
        this.gameObject.GetComponentInChildren<BoxCollider2D>().enabled = true;
        Invoke("BoxColliderOff", 0.1f);
    }
    void BoxColliderOff() { this.gameObject.GetComponentInChildren<BoxCollider2D>().enabled = false; }

    void MakeChargeColliderCharge() // 차징 - 괭이 물뿌리개
    { // 등급이 2이상이면 클릭하거나 누르고 있을때, 차징 시간이 계산되며, 뗐을때 조건에 맞는 크기의 콜라이더가 생성된다.
        

        if (Input.GetMouseButton(0)) // 누르고 있는동안 아래를 실행
        {
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
            else
            {
                colSize = new Vector2(0.8f, 0.8f);
                chargeLevel = 1f;
            }
        }
        if (Input.GetMouseButtonUp(0)) // 마우스를 떼었을때 Act
        {
            chargeTime = 0f;
            //Invoke("MakeBoxCollider", 0.5f);
            //chargeTool = false;
            Invoke("StaminaUse", 0.3f); ;
            Invoke("MakeBoxCollider", 0.3f);
            pCon.Charge(false);
            pCon.Motion(true);
        }
    }

    void MakeThrowColliderCharge()
    {
        pCon.FishCharge(true);

        colSize = new Vector2(0.8f, 0.8f); // 박스의 크기를 0.8 * 0.8로
        if (Input.GetMouseButton(0))
        {
            chargeTime += Time.deltaTime; // 2초일때 풀 차징
            if (chargeTime > 4f) { chargeTime = 0f; } //4초 이상에서 초기화
            else if (chargeTime < 2f) //2초까지 증가
            { throwPower = chargeTime; }
            else { throwPower = 4f - chargeTime; } //2~4초 감소
            chargeLevel = throwPower * 5f; // 박스의 생성위치를 throwpower * 5만큼 전방으로 = 최대 10f;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("RodThrow");
            Invoke("MakeThrowColliderAct", 0.5f); // throwpower에 비례해 던지는 시간 길어짐
        }

    }
    void MakeThrowColliderAct() // 차징 후, 모션 후 콜라이더 생성 - 낚싯대
    {
        pCon.FishCharge(false);
        StaminaUse();
        MakeBoxCollider();
        Invoke("ResetThrowColliderAct", 1.5f);
    }
    void ResetThrowColliderAct()
    {
        throwPower = 0f;
        chargeTime = 0f;
        chargeLevel = 0f;
    }

    void WaterLevel() // 물뿌리개
    {
        // 얘가 자기 앞의 오브젝트? 레이어? 아무튼 판정해서 물이면 나의 물 레벨을 채운다... 인데
    }
}
