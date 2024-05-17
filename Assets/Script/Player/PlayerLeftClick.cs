using System;
using System.Threading;
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
    ItemDB currentData;
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

        this.gameObject.GetComponentInChildren<EdgeCollider2D>().enabled = false;
    }
    private void Update()
    {
        Debug.Log(toolUsed);
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
    void UseAxe() // �Ϸ�
    {
        DoMotion();
        StaminaUse();
        Invoke("MakeCollider", 0.5f); // 0.5f�� �⺻������ ������ ���� ��� �ð��̴�.
    }

    void UsePickAxe() // �Ϸ�
    {
        DoMotion();
        StaminaUse();
        Invoke("MakeCollider", 0.5f);
    }

    void UseHoe() // �̿� : boxcollider
    {
        if(currentData.grade == 1) // ��� 1�ܰ� �϶� ������ ���� �ൿ
        {
            DoMotion();
            StaminaUse();
            Invoke("MakeCollider", 0.5f);
        }
        else // �� �ܿ� ��¡ ��, ��� �� �ݶ��̴� ����
        {
            MakeChargeColliderCharge();
        }
    }
    
    void UseWaterCan() // �̿� : boxcollider
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
    

    void StaminaUse()// �÷��̾� ��Ʈ�ѿ� ü�� ���׹̳� ����
    {
        pCon.currentStamina -= currentData.staminaRestor;
    }

    
    void DoMotion() // ���(�ֵθ���), �ֵθ��� �ð��� ���� ���� 0.5�ʷ� ����
    {
        toolUsed = true;
    }

    
    void MakeCollider() // ��� '��' �ݶ��̴� ����, ���� ��� ����
    {   this.gameObject.GetComponentInChildren<EdgeCollider2D>().enabled = true;
        toolUsed = false;
        Invoke("ColliderOff", 0.1f);
    }

    void MakeChargeColliderCharge() // ��¡ - ���� ���Ѹ���
    { // ����� 2�̻��̸� Ŭ���ϰų� ������ ������, ��¡ �ð��� ���Ǹ�, ������ ���ǿ� �´� ũ���� �ݶ��̴��� �����ȴ�.
        if (Input.GetMouseButton(0)) // ������ �ִµ��� �Ʒ��� ����
        {
            chargeTool = true;// �������� ��¡���¸� true�� ����
            chargeTime += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0)) // ���콺�� �������� Act
        {
            StaminaUse();
            DoMotion();
            if (chargeTime < 1f) // ��¡�� 1�� �̸��̶�� �Ϲ� ���� ����
            {
                StaminaUse();
                Invoke("MakeCollider", 0.5f);
            }
            else
            {
                Invoke("MakeChargeColliderAct", 0.5f);
            }
            chargeTool = false;
        }
    }

    void MakeChargeColliderAct() // ��¡ ��, ��� �� �ݶ��̴� ���� - ���� ���Ѹ���
    {
        if (chargeTime >= 1f && currentData.grade >= 2) //1�ܰ� ����, ��� 2 �̻�� 3ĭ
        {
            //this.gameObject.GetComponentInChildren<BoxCollider2D>().enabled = true; // ��� �ݶ��̴��� Ų��. �ٵ� ������ �ϴ� ������.
            //�ݶ��̴� ũ�� ���� -> getchildcomponent<boxcollider>.size
            //�ݶ��̴� ��ġ ���� -> transform.position
        }
        else if (chargeTime >= 2f && currentData.grade >= 3) //2�ܰ� ���� 5ĭ
        {

        }
        else if (chargeTime >= 3f && currentData.grade >= 4) //3�ܰ� ���� 9ĭ
        {

        }
        else if (chargeTime >= 4f && currentData.grade >= 5) //4�ܰ� ���� 18ĭ
        {

        }
        chargeTime = 0f;
        toolUsed = false;
        Invoke("ColliderOff", 0.1f);
    }

    void MakeThrowColliderCharge()  // ��¡ - ���˴�
    {
        chargeFishing = true; // ���˴� ��¡��
        if (Input.GetMouseButton(0))
        {
            chargeTime += Time.deltaTime; // 1���϶� Ǯ ��¡
            if (chargeTime > 2f) { chargeTime = 0f; } //2�� �̻󿡼� �ʱ�ȭ
            else if (chargeTime < 1f) //1�ʱ��� ����
            { throwPower = chargeTime; }
            else { throwPower = 2f - chargeTime; } //1~2�� ����
        }
        if (Input.GetMouseButtonUp(0))
        {
            DoMotion();
            Invoke("MakeThrowColliderAct", 0.5f); // throwpower�� ����� ������ �ð� �����
            chargeFishing = false;
        }
        
    }

    void MakeThrowColliderAct() // ��¡ ��, ��� �� �ݶ��̴� ���� - ���˴�
    {
        Debug.Log(throwPower);
        Debug.Log("Throw bobble");

        // throwpower�� �´� �Ÿ��� collider ����
        
        StaminaUse();
        throwPower = 0f;
        chargeTime = 0f;
        chargeFishing = false; // ���˴� ��¡�� �ƴ�
        toolUsed = false;
        Invoke("ColliderOff", 0.1f);
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
        Debug.Log("SwingColliderEnd");
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
}
