using System;
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

        // ������ ����� �������� �ð� üũ
        if (toolUsed) { passedTime += Time.deltaTime; }
        CoolDown();

        if (onCharge && Input.GetMouseButtonUp(0)) // �������� ���¿��� ��Ŭ���� ���ٸ�
        {   //���˴� �̰ų�, �ٸ� �����̰ų�

            if (currentData.toolType == 9) // ���˴���
            {
                MakeThrowColliderAct(); // ������ �ݶ��̴� ����
            }
            else    // �� �ܶ��
            {
                MakeChargeColliderAct(); // ���� �ݶ��̴� ����
            }
            onCharge = false;
        }

        if (this.gameObject.GetComponentInChildren<EdgeCollider2D>().enabled == true && !toolSwing) // ������ �����µ� �ֵθ��°� �ƴϸ� = �ݶ��̴��� ��� ������ �ʴ� �η�
        { Invoke("ColliderOff", 0.1f); }        // ��ٷ� ����
        else if (toolSwing)                     // �ݶ��̴��� ��� ����� �η�
        { Debug.Log("ASD"); Invoke("ColliderOff", coolDownTime);  // ��Ÿ���� ������ ����, 
            toolSwing = false;}                 // �ֵθ��⵵ ����
    }
    private void LateUpdate()
    {

    }
    void UseAxe() // �Ϸ�
    {
        Invoke("MakeCollider", coolDownTime);
        StopPlayer();
        StaminaUse();
    }
    //n���� �ٶ󺸴� ���⿡ �ݶ��̴� ���� �� ����, �ֵθ��� ���� �ٸ� ������ �Ұ�, ���׹̳� �Ҹ�
    void UseHoe() // �̿� : boxcollider
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
    //charge�� �� ��ġ�� �������� ���濡 �ټ��� �ݶ��̴� ���� �� ����,n���� �ٶ󺸴� ���⿡ �ݶ��̴� ���� �� ����,
    //��¡�� ������ ����, ����� �ٸ� ������ �Ұ�, ���׹̳� �Ҹ�
    void UseWaterCan() // �̿� : boxcollider
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
    //charge�� �� ��ġ�� �������� ���濡 �ټ��� �ݶ��̴� ���� �� ����,n���� �ٶ󺸴� ���⿡ �ݶ��̴� ���� �� ����,
    //��¡�� ������ ����, ����� �ٸ� ������ �Ұ�, ���׹̳� �Ҹ�
    //�� ä��� ����
    void UsePickAxe() // �Ϸ�
    {
        Invoke("MakeCollider", coolDownTime);
        StopPlayer();
        StaminaUse();
    }
    //n�ʵ��� �����̴� �ݶ��̴� ���� ���� ����, �ֵθ��� ���� �ٸ� ������ �Ұ�, ���׹̳� �Ҹ�
    void UseSickle()
    {
        StopPlayer();
        MakeMovingColliderSickle();
    }
    //n���� �ٶ󺸴� ���⿡ �ݶ��̴� ���� �� ����, �ֵθ��� ���� �ٸ� ������ �Ұ�
    void UseFishingRod()
    {
        MakeThrowColliderCharge();
    }
    //charge�� ���濡 �ݶ��̴� ����, 

    void StaminaUse()// �÷��̾� ��Ʈ�ѿ� ü�� ���׹̳� ����
    {
        pCon.currentStamina -= currentData.staminaRestor; // �÷��̾� ��Ʈ���� ���� ���׹̳ʰ� ��������۵������� ���׹̳� ȸ������ŭ ����
    }

    void StopPlayer() { toolUsed = true; } // toolUsed�� true�϶� �������Ұ�, �̵��Ұ�, ���

    void MakeCollider() //�����ݶ��̴� Ŵ, ��ġ, ȸ���� movement����
    { this.gameObject.GetComponentInChildren<EdgeCollider2D>().enabled = true; }

    void MakeChargeColliderCharge() //������ �ڽ� �ݶ��̴��� ����, �ʿ�� ũ�� ��ġ ����
    { // ����� 2�̻��̸� Ŭ���ϰų� ������ ������, ��¡ �ð��� ���Ǹ�, ������ ���ǿ� �´� ũ���� �ݶ��̴��� �����ȴ�.
        if (Input.GetMouseButton(0)) // ������ �ִµ��� �Ʒ��� ����
        {
            this.gameObject.GetComponent<PlayerMovement>().chargeMove = true;// �������� ��¡���¸� true�� ����
            chargeTime += Time.deltaTime;
            onCharge = true; // ��¡ ����� ������ ���� bool
        }

    } // ������ �ڽ��ݶ��̴�

    void MakeChargeColliderAct()
    {
        if (chargeTime < 1f)
        {
            MakeCollider();
        }
        else if (chargeTime >= 1f && currentData.grade >= 2) //1�ܰ� ����, ��� 2 �̻�� 3ĭ
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
        StaminaUse();
        StopPlayer();
        chargeTime = 0f;
        this.gameObject.GetComponent<PlayerMovement>().chargeMove = false;
    }

    void MakeThrowColliderCharge()  //���˴��϶��� ������ ��¡, �ٸ� ��¡������ �ٸ��� �����̴°� �Ұ�
                                    //��¡�Ŀ��� ���
    {
        pMov.chargeFishing = true; // ���˴� ��¡��
        chargeTime += Time.deltaTime; // 1��? 2��? �϶� Ǯ ��¡
        if (chargeTime > 2f) { chargeTime = 0f; } //2�� �̻󿡼� �ʱ�ȭ
        else if (chargeTime < 1f) //1�ʱ��� ����
        {throwPower = chargeTime;}
        else { throwPower = 2f - chargeTime; } //1~2�� ����
        
        onCharge = true; // ��¡����� �ൿ�� ���� bool
    }

    void MakeThrowColliderAct() //���˴���, ��¡�Ŀ��� ���� �� ������ġ ����
    {
        Debug.Log(throwPower);
        Debug.Log("Throw bobble");
        pMov.chargeFishing = false; // ���˴� ��¡�� �ƴ�
        StaminaUse();
        StopPlayer();
        throwPower = 0f;
        chargeTime = 0f;
    }
    void MakeMovingColliderSickle() //��
    {
        this.gameObject.GetComponentInChildren<EdgeCollider2D>().enabled = true;
        toolSwing = true;
    }
    void MakeMovingColliderWeapon() //����
    {
        this.gameObject.GetComponentInChildren<EdgeCollider2D>().enabled = true;
    }

    void WaterLevel() //���Ѹ���
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
