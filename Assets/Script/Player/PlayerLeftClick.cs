using System;
using System.Threading;
using System.Threading.Tasks;
using Unity;
using Unity.VisualScripting;
using UnityEngine;

class PlayerLeftClick : MonoBehaviour
// �긦 �÷��̾� ��Ʈ�ѿ� �ְ�, ��Ÿ���� �޾ƿ´�
{                       // �� Ÿ�Կ� �´� �޼��带 �����Ѵ�
                        // �׳� �� ��ü�� update�� �����ص� �Ǵ°� �ƴѰ�?

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
    public bool toolUsed;
    public bool toolSwing;
    public bool chargeTool;
    public bool Fishing;
    bool checkMouse;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        pCon = this.gameObject.GetComponent<PlayerController>();
        pMov = this.gameObject.GetComponent<PlayerMovement>();
        pInven = this.gameObject.GetComponent<PlayerInventroy>();
        chargedHitBox = this.GetComponentInChildren<BoxCollider2D>().transform.gameObject; // �� ���� ������Ʈ�� �ڽ��� �ڽ��ݶ��̴��� ã�Ƽ� �׳��� ���ӿ�����Ʈ�� ��ȯ
        chargedHitBox.GetComponent<BoxCollider2D>().enabled = false;
        swingHitBox = this.GetComponentInChildren<EdgeCollider2D>().transform.gameObject;
        swingHitBox.GetComponent<EdgeCollider2D>().enabled = false; 
    }
    private void Update()
    {
        
        currentData = new ItemDB(pInven.currentInventoryItem);

        if(gameManager.isInventoryOn == false)
        {
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

            if (toolSwing == true)
            {
                passedTime += Time.deltaTime;
                if (passedTime > 0.5f) { ColliderSwingOff(); }
            }
        }
        else if (gameManager.isInventoryOn == true)
        {
            OnInventoryClick();
        }
    }
    private void LateUpdate()
    {

    }
    void UseAxe() // �Ϸ�
    {
        DoMotion();
        StaminaUse();
        Invoke("MakeBoxCollider", 0.5f); // 0.5f�� �⺻������ ������ ���� ��� �ð��̴�.
    }

    void UsePickAxe() // �Ϸ�
    {
        DoMotion();
        StaminaUse();
        Invoke("MakeBoxCollider", 0.5f);
    }

    void UseHoe() 
    {
        if(currentData.grade == 1) // 1�ܰ� �϶� ������ ���� �ൿ
        {
            DoMotion();
            StaminaUse();
            Invoke("MakeBoxCollider", 0.5f);
        }
        else // �� �ܿ� ��¡ ��, ��� �� �ݶ��̴� ����
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
            Invoke("MakeBoxCollider", 0.5f);
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
    

    void StaminaUse()// �÷��̾� ��Ʈ�ѿ� ü�� ���׹̳� ����
    {pCon.currentStamina -= currentData.staminaRestor;}

    
    void DoMotion() // ���(�ֵθ���), �ֵθ��� �ð��� ���� ���� 0.5�ʷ� ����
    {toolUsed = true;}

    
    void MakeBoxCollider() // ��� '��' �ݶ��̴� ����, ���� ��� ����
    {   this.gameObject.GetComponentInChildren<BoxCollider2D>().enabled = true;
        toolUsed = false;
        Invoke("BoxColliderOff", 0.1f);
    }

    void MakeChargeColliderCharge() // ��¡ - ���� ���Ѹ���
    { // ����� 2�̻��̸� Ŭ���ϰų� ������ ������, ��¡ �ð��� ���Ǹ�, ������ ���ǿ� �´� ũ���� �ݶ��̴��� �����ȴ�.
        if (Input.GetMouseButton(0)) // ������ �ִµ��� �Ʒ��� ����
        {
            chargeTool = true;// �������� ��¡���¸� true�� ����

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
            StaminaUse();
            DoMotion();
            if (chargeTime < 1f) // ��¡�� 1�� �̸��̶�� �Ϲ� ���� ����
            {
                chargeTime = 0f;
                Invoke("MakeBoxCollider", 0.5f);
                chargeTool = false;
            }
            else
            {
                Invoke("MakeChargeColliderAct", 0.5f);
            }
        }
    }

    void MakeChargeColliderAct() // ��¡ ��, ��� �� �ݶ��̴� ���� - ���� ���Ѹ���
    {
        chargedHitBox.GetComponent<BoxCollider2D>().enabled = true;
        Invoke("AfterMakeChargeColliderAct", 0.1f);
    }
    void AfterMakeChargeColliderAct()
    {
        chargeTime = 0f;
        chargeTool = false;
        toolUsed = false;
        Invoke("ColliderOff", 0.1f);
        Invoke("BoxColliderOff", 0.1f);
    }

    void MakeThrowColliderCharge()  //
    {
        Fishing = true; // ������ : ��
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
            DoMotion();
            Invoke("MakeThrowColliderAct", 0.5f); // throwpower�� ����� ������ �ð� �����
        }
        
    }
    void MakeThrowColliderAct() // ��¡ ��, ��� �� �ݶ��̴� ���� - ���˴�
    {
        StaminaUse();
        chargedHitBox.GetComponent<BoxCollider2D>().enabled = true; // �ݶ��̴� ����;
        Invoke("AfterThrowColliderAct", 0.1f);
    }
    void AfterThrowColliderAct()
    {
        BoxColliderOff();
        throwPower = 0f;
        chargeTime = 0f;
        chargeLevel = 0f;
        Fishing = false;
        toolUsed = false;
    }
    void MakeMovingColliderSwing() // ��� �� �ݶ��̴� ����
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
    

    void WaterLevel() // ���Ѹ���
    {
        // �갡 �ڱ� ���� ������Ʈ? ���̾�? �ƹ�ư �����ؼ� ���̸� ���� �� ������ ä���... �ε�
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


    void OnInventoryClick()
    {

    }
}
