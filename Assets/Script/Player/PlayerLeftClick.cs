using System;
using System.Threading;
using System.Threading.Tasks;
using Unity;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

class PlayerLeftClick : MonoBehaviour // swingTool �̿ϼ�, ���Ѹ��� ������ �̿ϼ�
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

        if (pCon.idle || pCon.moving) // ��� ���� �̰ų� �����϶��� ��Ŭ���� ������ �ִ�.
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
        if (pCon.fishCharge) // ���˴� ��¡ �� �϶���
        {   //���˴븦 ��¡�ϰų� ������ �ִ�.
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
        if (pCon.charge) // ��¡ ���¿����� ��¡�� Ű��Ű�� �����ϴ�.
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
        if (pCon.motion) // ������϶� 
        {   //��Ÿ�� ī��Ʈ�� �ϰ�, ��� ���·� ���ư���. ���� ��Ÿ�� 0.5��. 
            //�ݶ��̴��� 0.3�ʿ� ���ܼ� 0.4�ʿ� �������, ��� ����� 0.5���̴�.
            motiontime = motiontime + Time.deltaTime;
            if (motiontime > 0.5f) { motiontime = 0f; pCon.Motion(false); }
        }
        // ��� ���� �̿ܿ��� ��Ŭ���� �ص� ȿ���� ����.
    }

    float motiontime;
    void UseAxe() // �Ϸ�
    {
        //DoMotion();
        //StaminaUse();
        //Invoke("MakeBoxCollider", 0.5f);
        pCon.Motion(true);
        Invoke("StaminaUse", 0.3f); ;
        Invoke("MakeBoxCollider", 0.3f);
    }

    void UsePickAxe() // �Ϸ�
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
        if (currentData.grade == 1) // 1�ܰ� �϶� ������ ���� �ൿ
        {
            //DoMotion();
            //StaminaUse();
            //Invoke("MakeBoxCollider", 0.5f);
            pCon.Motion(true);
            Invoke("StaminaUse", 0.3f); ;
            Invoke("MakeBoxCollider", 0.3f);
        }
        else // �� �ܿ� ��¡ ��, ��� �� �ݶ��̴� ����
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

    void StaminaUse()// �÷��̾� ��Ʈ�ѿ� ü�� ���׹̳� ����
    { pCon.currentStamina -= currentData.staminaRestor; }
    void MakeBoxCollider()
    {
        this.gameObject.GetComponentInChildren<BoxCollider2D>().enabled = true;
        Invoke("BoxColliderOff", 0.1f);
    }
    void BoxColliderOff() { this.gameObject.GetComponentInChildren<BoxCollider2D>().enabled = false; }

    void MakeChargeColliderCharge() // ��¡ - ���� ���Ѹ���
    { // ����� 2�̻��̸� Ŭ���ϰų� ������ ������, ��¡ �ð��� ���Ǹ�, ������ ���ǿ� �´� ũ���� �ݶ��̴��� �����ȴ�.
        

        if (Input.GetMouseButton(0)) // ������ �ִµ��� �Ʒ��� ����
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
        if (Input.GetMouseButtonUp(0)) // ���콺�� �������� Act
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

        colSize = new Vector2(0.8f, 0.8f); // �ڽ��� ũ�⸦ 0.8 * 0.8��
        if (Input.GetMouseButton(0))
        {
            chargeTime += Time.deltaTime; // 2���϶� Ǯ ��¡
            if (chargeTime > 4f) { chargeTime = 0f; } //4�� �̻󿡼� �ʱ�ȭ
            else if (chargeTime < 2f) //2�ʱ��� ����
            { throwPower = chargeTime; }
            else { throwPower = 4f - chargeTime; } //2~4�� ����
            chargeLevel = throwPower * 5f; // �ڽ��� ������ġ�� throwpower * 5��ŭ �������� = �ִ� 10f;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("RodThrow");
            Invoke("MakeThrowColliderAct", 0.5f); // throwpower�� ����� ������ �ð� �����
        }

    }
    void MakeThrowColliderAct() // ��¡ ��, ��� �� �ݶ��̴� ���� - ���˴�
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

    void WaterLevel() // ���Ѹ���
    {
        // �갡 �ڱ� ���� ������Ʈ? ���̾�? �ƹ�ư �����ؼ� ���̸� ���� �� ������ ä���... �ε�
    }
}
